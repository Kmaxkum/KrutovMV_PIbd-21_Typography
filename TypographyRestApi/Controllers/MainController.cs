using TypographyRestApi.Services;
using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.Interfaces;
using TypographyServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace TypographyRestAPI.Controllers
{
    public class MainController : ApiController
    {
        private readonly IMainService _service;
        private readonly IWorkerService _serviceImplementer;
        public MainController(IMainService service, IWorkerService serviceImplementer)
        {
            _service = service;
            _serviceImplementer = serviceImplementer;
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
        public void PayOrder(BookingBindingModel model)
        {
            _service.PayOrder(model);
        }
        [HttpPost]
        public void PutComponentOnStock(StoragePartBindingModel model)
        {
            _service.PutComponentOnStock(model);
        }
        [HttpPost]
        public void StartWork()
        {
            List<BookingViewModel> orders = _service.GetFreeOrders();
            foreach (var order in orders)
            {
                WorkerViewModel impl = _serviceImplementer.GetFreeWorker();
                if (impl == null)
                {
                    throw new Exception("Нет сотрудников");
                }
                new WorkWorker(_service, _serviceImplementer, impl.Id, order.Id);
            }
        }
    }
}
