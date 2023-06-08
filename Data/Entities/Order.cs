using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.Entities.product;

namespace Data.Entities
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public Customers Customers { get; set; }
        public int ProductId { get; set; }
        public product Product { get; set; }
        [Required]
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        [Required]
        [MinLength(1), MaxLength(200)]
        public string ShippingAddress { get; set; }
        public bool IsShipped { get; set; }
        public string PaymentMethod { get; set; }


    }
}
