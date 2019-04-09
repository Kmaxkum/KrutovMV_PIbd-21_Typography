using TypographyModel;
using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.Interfaces;
using TypographyServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TypographyServiceImplementList.Implementations
{
    public class MainServiceList : IMainService
    {
        private DataListSingleton source;
        public MainServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<BookingViewModel> GetList()
        {
            List<BookingViewModel> result = source.Bookings
                .Select(rec => new BookingViewModel
                {
                    Id = rec.Id,
                    CustomerId = rec.CustomerId,
                    ItemId = rec.ItemId,
                    DateCreate = rec.DateCreate.ToLongDateString(),
                    DateImplement = rec.DateImplement?.ToLongDateString(),
                    State = rec.State.ToString(),
                    Cnt = rec.Cnt,
                    TotalSum = rec.TotalSum,
                    CustomerFIO = source.Customers.FirstOrDefault(recC => recC.Id == rec.CustomerId)?.CustomerFIO,
                    ItemName = source.Items.FirstOrDefault(recP => recP.Id == rec.ItemId)?.ItemName,
                })
                .ToList();
            return result;
        }
        public void CreateOrder(BookingBindingModel model)
        {
            int maxId = source.Bookings.Count > 0 ? source.Bookings.Max(rec => rec.Id) : 0;
            source.Bookings.Add(new Booking
            {
                Id = maxId + 1,
                CustomerId = model.CustomerId,
                ItemId = model.ItemId,
                DateCreate = DateTime.Now,
                Cnt = model.Cnt,
                TotalSum = model.TotalSum,
                State = BookingStatus.Принят
            });
        }
        public void TakeOrderInWork(BookingBindingModel model)
        {
            Booking element = source.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.State != BookingStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            // смотрим по количеству компонентов на складах
            var itemParts = source.ItemParts.Where(rec => rec.ItemId == element.ItemId);
            foreach (var itemPart in itemParts)
            {
                int countOnStorage = source.StorageParts
                    .Where(rec => rec.PartId == itemPart.PartId)
                    .Sum(rec => rec.Cnt);
                if (countOnStorage < itemPart.Cnt * element.Cnt)
                {
                    var componentName = source.Parts.FirstOrDefault(rec => rec.Id ==
                   itemPart.PartId);
                    throw new Exception("Не достаточно компонента " +
                   componentName?.PartName + " требуется " + (itemPart.Cnt * element.Cnt) +
                   ", в наличии " + countOnStorage);
                }
            }
            // списываем
            foreach (var itemPart in itemParts)
            {
                int countOnStorage = itemPart.Cnt * element.Cnt;
                var stockParts = source.StorageParts.Where(rec => rec.PartId
               == itemPart.PartId);
                foreach (var storagePart in stockParts)
                {
                    // компонентов на одном слкаде может не хватать
                    if (storagePart.Cnt >= countOnStorage)
                    {
                        storagePart.Cnt -= countOnStorage;
                        break;
                    }
                    else
                    {
                        countOnStorage -= storagePart.Cnt;
                        storagePart.Cnt = 0;
                    }
                }
            }
            element.DateImplement = DateTime.Now;
            element.State = BookingStatus.Выполняется;
        }
        public void FinishOrder(BookingBindingModel model)
        {
            Booking element = source.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.State != BookingStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.State = BookingStatus.Готов;

        }
        public void PayOrder(BookingBindingModel model)
        {
            Booking element = source.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.State != BookingStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.State = BookingStatus.Оплачен;
        }
        public void PutComponentOnStock(StoragePartBindingModel model)
        {
            StoragePart element = source.StorageParts.FirstOrDefault(rec =>
            rec.StorageId == model.StorageId && rec.PartId == model.PartId);
            if (element != null)
            {
                element.Cnt += model.Cnt;
            }
            else
            {
                int maxId = source.StorageParts.Count > 0 ?
               source.StorageParts.Max(rec => rec.Id) : 0;
                source.StorageParts.Add(new StoragePart
                {
                    Id = ++maxId,
                    StorageId = model.StorageId,
                    PartId = model.PartId,
                    Cnt = model.Cnt
                });
            }
        }
    }
}
