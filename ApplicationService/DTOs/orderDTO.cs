using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs
{
    public class orderDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }=DateTime.Now;
        public int CustomerId { get; set; }
        public customerDTO Customers { get; set; }
        public int ProductId { get; set; }
        public productDTO Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string ShippingAddress { get; set; }
        public bool IsShipped { get; set; }
        public string PaymentMethod { get; set; }


        public orderDTO()
        {
            Customers = new customerDTO();
            Product = new productDTO();
        }
        public bool Validate()
        {
            return true;// !String.IsNullOrEmpty(ShippingAddress)
               //  && CustomerId != 0 &&
               //  ProductId != 0;
        }



    }
}
