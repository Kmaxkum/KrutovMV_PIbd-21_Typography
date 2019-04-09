using TypographyModel;
using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.Interfaces;
using TypographyServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TypographyServiceImplementList.Implementations
{
    public class ItemServiceList : IItemService
    {
        private DataListSingleton source;
        public ItemServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<ItemViewModel> GetList()
        {
            List<ItemViewModel> result = source.Items
                .Select(rec => new ItemViewModel
                {
                    Id = rec.Id,
                    ItemName = rec.ItemName,
                    Cost = rec.Cost,
                    ItemPart = source.ItemParts
                        .Where(recPC => recPC.ItemId == rec.Id)
                        .Select(recPC => new ItemPartViewModel
                        {
                            Id = recPC.Id,
                            ItemId = recPC.ItemId,
                            PartId = recPC.PartId,
                            PartName = source.Parts.FirstOrDefault(recC =>
                            recC.Id == recPC.PartId)?.PartName,
                            Cnt = recPC.Cnt
                        })
                        .ToList()
                })
                .ToList();
            return result;
        }
        public ItemViewModel GetElement(int id)
        {
            Item element = source.Items.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ItemViewModel
                {
                    Id = element.Id,
                    ItemName = element.ItemName,
                    Cost = element.Cost,
                    ItemPart = source.ItemParts
                .Where(recPC => recPC.ItemId == element.Id)
                .Select(recPC => new ItemPartViewModel
                {
                    Id = recPC.Id,
                    ItemId = recPC.ItemId,
                    PartId = recPC.PartId,
                    PartName = source.Parts.FirstOrDefault(recC =>
     recC.Id == recPC.PartId)?.PartName,
                    Cnt = recPC.Cnt
                })
               .ToList()
                };
            }
            throw new Exception("Элемент не найден");

        }
        public void AddElement(ItemBindingModel model)
        {
            Item element = source.Items.FirstOrDefault(rec => rec.ItemName ==
model.ItemName);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            int maxId = source.Items.Count > 0 ? source.Items.Max(rec => rec.Id) :
           0;
            source.Items.Add(new Item
            {
                Id = maxId + 1,
                ItemName = model.ItemName,
                Cost = model.Cost
            });
            // компоненты для изделия
            int maxPCId = source.ItemParts.Count > 0 ?
           source.ItemParts.Max(rec => rec.Id) : 0;
            // убираем дубли по компонентам
            var groupComponents = model.ItemPart
            .GroupBy(rec => rec.PartId)
           .Select(rec => new
           {
               PartId = rec.Key,
               Cnt = rec.Sum(r => r.Cnt)
           });
            // добавляем компоненты
            foreach (var groupComponent in groupComponents)
            {
                source.ItemParts.Add(new ItemPart
                {
                    Id = ++maxPCId,
                    ItemId = maxId + 1,
                    PartId = groupComponent.PartId,
                    Cnt = groupComponent.Cnt
                });
            }
        }

        public void UpdElement(ItemBindingModel model)
        {
            Item element = source.Items.FirstOrDefault(rec => rec.ItemName ==
model.ItemName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            element = source.Items.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ItemName = model.ItemName;
            element.Cost = model.Cost;
            int maxPCId = source.ItemParts.Count > 0 ?
           source.ItemParts.Max(rec => rec.Id) : 0;
            // обновляем существуюущие компоненты
            var compIds = model.ItemPart.Select(rec =>
           rec.PartId).Distinct();
            var updateComponents = source.ItemParts.Where(rec => rec.ItemId ==
           model.Id && compIds.Contains(rec.PartId));
            foreach (var updateComponent in updateComponents)
            {
                updateComponent.Cnt = model.ItemPart.FirstOrDefault(rec =>
               rec.Id == updateComponent.Id).Cnt;
            }
            source.ItemParts.RemoveAll(rec => rec.ItemId == model.Id &&
           !compIds.Contains(rec.PartId));
            // новые записи
            var groupComponents = model.ItemPart
            .Where(rec => rec.Id == 0)
           .GroupBy(rec => rec.PartId)
           .Select(rec => new
           {
               PartId = rec.Key,
               Cnt = rec.Sum(r => r.Cnt)
           });
            foreach (var groupComponent in groupComponents)
            {
                ItemPart elementPC = source.ItemParts.FirstOrDefault(rec
               => rec.ItemId == model.Id && rec.PartId == groupComponent.PartId);
                if (elementPC != null)
                {
                    elementPC.Cnt += groupComponent.Cnt;
                }
                else
                {
                    source.ItemParts.Add(new ItemPart
                    {
                        Id = ++maxPCId,
                        ItemId = model.Id,
                        PartId = groupComponent.PartId,
                        Cnt = groupComponent.Cnt
                    });
                }
            }
        }
        public void DelElement(int id)
        {
            Item element = source.Items.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // удаяем записи по компонентам при удалении изделия
                source.ItemParts.RemoveAll(rec => rec.ItemId == id);
                source.Items.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
