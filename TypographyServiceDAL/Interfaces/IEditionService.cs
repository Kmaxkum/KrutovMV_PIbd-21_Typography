using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.ViewModels;
using System.Collections.Generic;

namespace TypographyServiceDAL.Interfaces
{
    public interface IEditionService
    {
        void SaveItemPrice(EditionBindingModel model);
        List<StorageLoadViewModel> GetStoragesLoad();
        void SaveStoragesLoad(EditionBindingModel model);
        List<CustomerBookingViewModel> GetCustomerBookings(EditionBindingModel model);
        void SaveCustomerBookings(EditionBindingModel model);
    }
}
