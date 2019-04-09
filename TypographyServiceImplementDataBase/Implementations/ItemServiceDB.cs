using TypographyModel;
using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.Interfaces;
using TypographyServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TypographyServiceImplementDataBase.Implementations
{
    public class ItemServiceDB : IItemService
    {
        private TypographyDbContext context;
        public ItemServiceDB(TypographyDbContext context)
        {
            this.context = context;
        }
        public List<ItemViewModel> GetList()
        {
            List<ItemViewModel> result = context.Items.Select(rec => new
            ItemViewModel
            {
                Id = rec.Id,
                ItemName = rec.ItemName,
                Cost = rec.Cost,
                ItemPart = context.ItemParts
                    .Where(recPC => recPC.ItemId == rec.Id)
                    .Select(recPC => new ItemPartViewModel
                    {
                        Id = recPC.Id,
                        ItemId = recPC.ItemId,
                        PartId = recPC.PartId,
                        PartName = recPC.Parts.PartName,
                        Cnt = recPC.Cnt
                    })
                    .ToList()
            })
            .ToList();
            return result;
        }
        public ItemViewModel GetElement(int id)
        {
            Item element = context.Items.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ItemViewModel
            {
                    Id = element.Id,
                    ItemName = element.ItemName,
                    Cost = element.Cost,
                    ItemPart = context.ItemParts
                        .Where(recPC => recPC.ItemId == element.Id)
                        .Select(recPC => new ItemPartViewModel
                        {
                            Id = recPC.Id,
                            ItemId = recPC.ItemId,
                            PartId = recPC.PartId,
                            PartName = recPC.Parts.PartName,
                            Cnt = recPC.Cnt
                        })
                        .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(ItemBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Item element = context.Items.FirstOrDefault(rec =>
                   rec.ItemName == model.ItemName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = new Item
                    {
                        ItemName = model.ItemName,
                        Cost = model.Cost
                    };
                    context.Items.Add(element);
                    context.SaveChanges();
                    // убираем дубли по компонентам
                    var groupParts = model.ItemPart
                     .GroupBy(rec => rec.PartId)
                    .Select(rec => new
                    {
                        PartId = rec.Key,
                        Count = rec.Sum(r => r.Cnt)
                    });
                    // добавляем компоненты
                    foreach (var groupPart in groupParts)
                    {
                        context.ItemParts.Add(new ItemPart
                        {
                            ItemId = element.Id,
                            PartId = groupPart.PartId,
                            Cnt = groupPart.Count
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void UpdElement(ItemBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Item element = context.Items.FirstOrDefault(rec =>
                   rec.ItemName == model.ItemName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Items.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.ItemName = model.ItemName;
                    element.Cost = model.Cost;
                    context.SaveChanges();
                    // обновляем существуюущие компоненты
                    var compIds = model.ItemPart.Select(rec =>
                   rec.PartId).Distinct();
                    var updateComponents = context.ItemParts.Where(rec =>
                   rec.ItemId == model.Id && compIds.Contains(rec.PartId));
                    foreach (var updateComponent in updateComponents)
                    {
                        updateComponent.Cnt =
                       model.ItemPart.FirstOrDefault(rec => rec.Id == updateComponent.Id).Cnt;
                    }
                    context.SaveChanges();
                    context.ItemParts.RemoveRange(context.ItemParts.Where(rec =>
                    rec.ItemId == model.Id && !compIds.Contains(rec.PartId)));
                    context.SaveChanges();
                    // новые записи
                    var groupParts = model.ItemPart
                    .Where(rec => rec.Id == 0)
                    .GroupBy(rec => rec.PartId)
                    .Select(rec => new
                    {
                        PartId = rec.Key,
                        Count = rec.Sum(r => r.Cnt)
                    });
                    foreach (var groupPart in groupParts)
                    {
                        ItemPart elementPC = context.ItemParts.FirstOrDefault(rec => rec.ItemId == model.Id && rec.PartId == groupPart.PartId);
                        if (elementPC != null)
                        {
                            elementPC.Cnt += groupPart.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.ItemParts.Add(new ItemPart
                            {
                                ItemId = model.Id,
                                PartId = groupPart.PartId,
                                Cnt = groupPart.Count
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Item element = context.Items.FirstOrDefault(rec => rec.Id ==
                   id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.ItemParts.RemoveRange(context.ItemParts.Where(rec =>
                        rec.ItemId == id));
                        context.Items.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
