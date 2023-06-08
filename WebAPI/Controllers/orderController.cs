using ApplicationService.DTOs;
using ApplicationService.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    [Authorize]
    public class orderController : ApiController
    {
        private OrderManagementService orderService = new OrderManagementService();

        [HttpGet]
        [AllowAnonymous]
        [Route("api/order")]
        public IHttpActionResult Get()
        {
            var orders = orderService.GetAllOrders();
            return Json(orders);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/order/{id}")]
        public IHttpActionResult Get(int id)
        {
            var order = orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Json(order);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/order")]
        public IHttpActionResult Save(orderDTO orderDTO)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (orderService.CreateOrder(orderDTO))
            {
                return Ok("Order is created.");
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("api/order/Edit/{id}")]
        public IHttpActionResult Update(int id, orderDTO orderDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            orderDTO.Id = id;
            if (orderService.UpdateOrder(orderDTO))
            {
                return Ok("Order is updated.");
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("api/order/{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (orderService.DeleteOrder(id))
            {
                return Ok("Order is deleted.");
            }
            else
            {
                return InternalServerError();
            }
        }
    }

}
