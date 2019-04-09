using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.ViewModels;
using System.Collections.Generic;

namespace TypographyServiceDAL.Interfaces
{
    public interface IStorageService
    {
        List<StorageViewModel> GetList();
        StorageViewModel GetElement(int id);
        void AddElement(StorageBindingModel model);
        void UpdElement(StorageBindingModel model);
        void DelElement(int id);
    }
}
