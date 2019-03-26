using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.ViewModels;
using System.Collections.Generic;

namespace TypographyServiceDAL.Interfaces
{
    public interface IItemService
    {
        List<ItemViewModel> GetList();
        ItemViewModel GetElement(int id);
        void AddElement(ItemBindingModel model);
        void UpdElement(ItemBindingModel model);
        void DelElement(int id);
    }
}
