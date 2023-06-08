using ApplicationService.DTOs;
using ApplicationService.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Messages;

namespace WebAPI.Controllers
{
    [Authorize]
    public class CustomerController : ApiController
    {
        private readonly CustomerManagementService _customerManagementService;

        public CustomerController()
        {
            _customerManagementService = new CustomerManagementService();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/customer")]
        public IHttpActionResult Get()
        {
            var customers = _customerManagementService.GetAllCustomers();
            return Json(customers);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/customer/{id}")]
        public IHttpActionResult Get(int id)
        {
            var customer = _customerManagementService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Json(customer);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/customer")]
        public IHttpActionResult Save([FromBody] customerDTO customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseMessage response = new ResponseMessage();

            if (_customerManagementService.CreateCustomer(customerDto))
            {
                response.Code = 200;
                response.Body = "Customer is created.";
            }
            else
            {
                response.Code = 500;
                response.Error = "Customer was not created.";
            }

            return Json(response);
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("api/customer/Edit/{id}")]
        public IHttpActionResult Edit(int id, [FromBody] customerDTO customerDto)
        {
            ResponseMessage response = new ResponseMessage();

            if (!ModelState.IsValid)
            {
                return Json(new ResponseMessage
                {
                    Code = 500,
                    Error = "Data is not valid!"
                });
            }

            customerDto.Id = id;
            if (_customerManagementService.UpdateCustomer(customerDto))
            {
                response.Code = 200;
                response.Body = "Customer is updated.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Customer was not updated.";
            }

            return Json(response);
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("api/customer/{id}")]
        public IHttpActionResult Delete(int id)
        {
            ResponseMessage response = new ResponseMessage();

            if (_customerManagementService.DeleteCustomer(id))
            {
                response.Code = 200;
                response.Body = "Customer is deleted.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Customer is not deleted.";
            }

            return Json(response);
        }
    }
}
