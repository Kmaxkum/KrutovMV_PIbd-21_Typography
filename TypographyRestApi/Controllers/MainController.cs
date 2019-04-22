using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.Interfaces;
using System;
using System.Web.Http;

namespace TypographyRestAPI.Controllers
{
    public class MainController : ApiController
    {
        private readonly IMainService _service;
        public MainController(IMainService service)
        {
            _service = service;
        }
        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
        [HttpPost]
        public void CreateOrder(BookingBindingModel model)
        {
            _service.CreateOrder(model);
        }
        [HttpPost]
        public void TakeOrderInWork(BookingBindingModel model)
        {
            _service.TakeOrderInWork(model);
        }
        [HttpPost]
        public void FinishOrder(BookingBindingModel model)
        {
            _service.FinishOrder(model);
        }
        [HttpPost]
        public void PayOrder(BookingBindingModel model)
        {
            _service.PayOrder(model);
        }
        [HttpPost]
        public void PutComponentOnStock(StoragePartBindingModel model)
        {
            _service.PutComponentOnStock(model);
        }
    }
}
