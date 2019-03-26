using TypographyModel;
using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.Interfaces;
using TypographyServiceDAL.ViewModels;
using System;
using System.Collections.Generic;

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
            List<ItemViewModel> result = new List<ItemViewModel>();
            for (int i = 0; i < source.Items.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<ItemPartViewModel> itemPart = new List<ItemPartViewModel>();
                for (int j = 0; j < source.ItemParts.Count; ++j)
                {
                    if (source.ItemParts[j].ItemId == source.Items[i].Id)
                    {
                        string partName = string.Empty;
                        for (int k = 0; k < source.Parts.Count; ++k)
                        {
                            if (source.ItemParts[j].PartId ==
                           source.Parts[k].Id)
                            {
                                partName = source.Parts[k].PartName;
                                break;
                            }
                        }
                        itemPart.Add(new ItemPartViewModel
                        {
                            Id = source.ItemParts[j].Id,
                            ItemId = source.ItemParts[j].ItemId,
                            PartId = source.ItemParts[j].PartId,
                            PartName = partName,
                            Cnt = source.ItemParts[j].Cnt
                        });
                    }
                }
                result.Add(new ItemViewModel
                {
                    Id = source.Items[i].Id,
                    ItemName = source.Items[i].ItemName,
                    Cost = source.Items[i].Cost,
                    ItemPart = itemPart
                });
            }
            return result;
        }
        public ItemViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Items.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
            List<ItemPartViewModel> itemPart = new List<ItemPartViewModel>();
                for (int j = 0; j < source.ItemParts.Count; ++j)
                {
                    if (source.ItemParts[j].ItemId == source.Items[i].Id)
                    {
                        string componentName = string.Empty;
                        for (int k = 0; k < source.Parts.Count; ++k)
                        {
                            if (source.ItemParts[j].PartId ==
                           source.Parts[k].Id)
                            {
                                componentName = source.Parts[k].PartName;
                                break;
                            }
                        }
                        itemPart.Add(new ItemPartViewModel
                        {
                            Id = source.ItemParts[j].Id,
                            ItemId = source.ItemParts[j].ItemId,
                            PartId = source.ItemParts[j].PartId,
                            PartName = componentName,
                            Cnt = source.ItemParts[j].Cnt
                        });
                    }
                }
                if (source.Items[i].Id == id)
                {
                    return new ItemViewModel
                    {
                        Id = source.Items[i].Id,
                        ItemName = source.Items[i].ItemName,
                        Cost = source.Items[i].Cost,
                        ItemPart = itemPart
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(ItemBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Items.Count; ++i)
            {
                if (source.Items[i].Id > maxId)
                {
                    maxId = source.Items[i].Id;
                }
                if (source.Items[i].ItemName == model.ItemName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.Items.Add(new Item
            {
                Id = maxId + 1,
                ItemName = model.ItemName,
                Cost = model.Cost
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.ItemParts.Count; ++i)
            {
                if (source.ItemParts[i].Id > maxPCId)
                {
                    maxPCId = source.ItemParts[i].Id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.ItemPart.Count; ++i)
            {
                for (int j = 1; j < model.ItemPart.Count; ++j)
                {
                    if (model.ItemPart[i].PartId ==
                    model.ItemPart[j].PartId)
                    {
                        model.ItemPart[i].Cnt +=
                        model.ItemPart[j].Cnt;
                        model.ItemPart.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты
            for (int i = 0; i < model.ItemPart.Count; ++i)
            {
                source.ItemParts.Add(new ItemPart
                {
                    Id = ++maxPCId,
                    ItemId = maxId + 1,
                    PartId = model.ItemPart[i].PartId,
                    Cnt = model.ItemPart[i].Cnt
                });
            }
        }
        public void UpdElement(ItemBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Items.Count; ++i)
            {
                if (source.Items[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Items[i].ItemName == model.ItemName &&
                source.Items[i].Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Items[index].ItemName = model.ItemName;
            source.Items[index].Cost = model.Cost;
            int maxPCId = 0;
            for (int i = 0; i < source.ItemParts.Count; ++i)
            {
                if (source.ItemParts[i].Id > maxPCId)
                {
                    maxPCId = source.ItemParts[i].Id;
                }
            }
            // обновляем существуюущие компоненты
            for (int i = 0; i < source.ItemParts.Count; ++i)
            {
                if (source.ItemParts[i].ItemId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.ItemPart.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.ItemParts[i].Id ==
                       model.ItemPart[j].Id)
                        {
                            source.ItemParts[i].Cnt =
                           model.ItemPart[j].Cnt;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if (flag)
                    {
                        source.ItemParts.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for (int i = 0; i < model.ItemPart.Count; ++i)
            {
                if (model.ItemPart[i].Id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.ItemParts.Count; ++j)
                    {
                        if (source.ItemParts[j].ItemId == model.Id &&
                        source.ItemParts[j].PartId ==
                       model.ItemPart[i].PartId)
                        {
                            source.ItemParts[j].Cnt +=
                           model.ItemPart[i].Cnt;
                            model.ItemPart[i].Id =
                           source.ItemParts[j].Id;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.ItemPart[i].Id == 0)
                    {
                        source.ItemParts.Add(new ItemPart
                        {
                            Id = ++maxPCId,
                            ItemId = model.Id,
                            PartId = model.ItemPart[i].PartId,
                            Cnt = model.ItemPart[i].Cnt
                        });
                    }
                }
            }
        }
        public void DelElement(int id)
        {
            // удаяем записи по компонентам при удалении изделия
            for (int i = 0; i < source.ItemParts.Count; ++i)
            {
                if (source.ItemParts[i].ItemId == id)
                {
                    source.ItemParts.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Items.Count; ++i)
            {
                if (source.Items[i].Id == id)
                {
                    source.Items.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
