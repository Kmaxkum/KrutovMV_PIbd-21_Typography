using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.Interfaces;
using System;
using System.Web.Http;

namespace TypographyRestAPI.Controllers
{
    public class EditionController : ApiController
    {
        private readonly IEditionService _service;
        public EditionController(IEditionService service)
        {
            _service = service;
        }
        [HttpGet]
        public IHttpActionResult GetStoragesLoad()
        {
            var list = _service.GetStoragesLoad();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
        [HttpPost]
        public IHttpActionResult GetCustomerBookings(EditionBindingModel model)
        {
            var list = _service.GetCustomerBookings(model);
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
        [HttpPost]
        public void SaveItemPrice(EditionBindingModel model)
        {
            _service.SaveItemPrice(model);
        }
        [HttpPost]
        public void SaveStoragesLoad(EditionBindingModel model)
        {
            _service.SaveStoragesLoad(model);
        }
        [HttpPost]
        public void SaveCustomerBookings(EditionBindingModel model)
        {
            _service.SaveCustomerBookings(model);
        }
    }
}
