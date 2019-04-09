using TypographyModel;
using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.Interfaces;
using TypographyServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace TypographyServiceImplementDataBase.Implementations
{
    public class MainServiceDB : IMainService
    {
        private TypographyDbContext context;
        public MainServiceDB(TypographyDbContext context)
        {
            this.context = context;
        }
        public List<BookingViewModel> GetList()
        {
            List<BookingViewModel> result = context.Bookings.Select(rec => new BookingViewModel
            {
                Id = rec.Id,
                CustomerId = rec.CustomerId,
                ItemId = rec.ItemId,
                DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
            SqlFunctions.DateName("mm", rec.DateCreate) + " " +
            SqlFunctions.DateName("yyyy", rec.DateCreate),
                DateImplement = rec.DateImplement == null ? "" :
            SqlFunctions.DateName("dd",
           rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("mm",
           rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("yyyy",
           rec.DateImplement.Value),
                State = rec.State.ToString(),
                Cnt = rec.Cnt,
                TotalSum = rec.TotalSum,
                CustomerFIO = rec.Customer.CustomerFIO,
                ItemName = rec.Item.ItemName
            })
            .ToList();
            return result;
        }
        public void CreateOrder(BookingBindingModel model)
        {
            context.Bookings.Add(new Booking
            {
                CustomerId = model.CustomerId,
                ItemId = model.ItemId,
                DateCreate = DateTime.Now,
                Cnt = model.Cnt,
                TotalSum = model.TotalSum,
                State = BookingStatus.Принят
            });
            context.SaveChanges();
        }
        public void TakeOrderInWork(BookingBindingModel model)
        {
        using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Booking element = context.Bookings.FirstOrDefault(rec => rec.Id ==
                    model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    if (element.State != BookingStatus.Принят)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    var productComponents = context.ItemParts.Include(rec =>
                    rec.Parts).Where(rec => rec.ItemId == element.ItemId);
                    // списываем
                    foreach (var itemPart in productComponents)
                    {
                        int countOnStorage = itemPart.Cnt * element.Cnt;
                        var storageParts = context.StorageParts.Where(rec =>
                        rec.PartId == itemPart.PartId);
                        foreach (var storagePart in storageParts)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (storagePart.Cnt >= countOnStorage)
                            {
                                storagePart.Cnt -= countOnStorage;
                                countOnStorage = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStorage -= storagePart.Cnt;
                                storagePart.Cnt = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStorage > 0)
                        {
                            throw new Exception("Не достаточно компонента " +
                            itemPart.Parts.PartName + " требуется " + itemPart.Cnt + ", нехватает " + countOnStorage);
                         }
                    }
                    element.DateImplement = DateTime.Now;
                    element.State = BookingStatus.Выполняется;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void FinishOrder(BookingBindingModel model)
        {
            Booking element = context.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                    throw new Exception("Элемент не найден");
                }
                if (element.State != BookingStatus.Выполняется)
                {
                    throw new Exception("Заказ не в статусе \"Выполняется\"");
                }
                element.State = BookingStatus.Готов;
                context.SaveChanges();
            }
        public void PayOrder(BookingBindingModel model)
        {
            Booking element = context.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.State != BookingStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.State = BookingStatus.Оплачен;
            context.SaveChanges();
        }
        public void PutComponentOnStock(StoragePartBindingModel model)
        {
            StoragePart element = context.StorageParts.FirstOrDefault(rec =>
            rec.StorageId == model.StorageId && rec.PartId == model.PartId);
            if (element != null)
            {
                element.Cnt += model.Cnt;
            }
            else
            {
                context.StorageParts.Add(new StoragePart
                {
                    StorageId = model.StorageId,
                    PartId = model.PartId,
                    Cnt = model.Cnt
                });
            }
            context.SaveChanges();
        }
    }
}
