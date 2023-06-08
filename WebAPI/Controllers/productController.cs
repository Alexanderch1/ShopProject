using System;
using System.Collections.Generic;
using System.Web.Http;
using ApplicationService.DTOs;
using ApplicationService.Implementations;
using WebAPI.Messages;

namespace WebAPI.Controllers
{
    [Authorize]
    public class ProductController : ApiController
    {
        
        private readonly ProductManagementService productService = new ProductManagementService();

        [HttpGet]
        [AllowAnonymous]
        [Route("api/product")]
        public IHttpActionResult Get()
        {
            var products = productService.Get();
            return Json(products);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/product/{id}")]
        public IHttpActionResult Get(int id)
        {
            var product = productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Json(product);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/product")]
        public IHttpActionResult Save(productDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productService.CreateProduct(productDTO))
            {
                return Ok("Product is created.");
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("api/product/Edit/{id}")]
        public IHttpActionResult Edit(int id, productDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            productDTO.Id = id;
            if (productService.UpdateProduct(productDTO))
            {
                return Ok("Product is updated.");
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("api/product/Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (productService.DeleteProduct(id))
            {
                return Ok("Product is deleted.");
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}

