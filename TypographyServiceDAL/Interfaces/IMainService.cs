using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.ViewModels;
using System.Collections.Generic;

namespace TypographyServiceDAL.Interfaces
{
    public interface IMainService
    {
        List<BookingViewModel> GetList();
        List<BookingViewModel> GetFreeOrders();
        void CreateOrder(BookingBindingModel model);
        void TakeOrderInWork(BookingBindingModel model);
        void FinishOrder(BookingBindingModel model);
        void PayOrder(BookingBindingModel model);
        void PutComponentOnStock(StoragePartBindingModel model);
    }
}
