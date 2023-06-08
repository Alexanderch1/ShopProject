using ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class orderVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the order date.")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Please select a customer.")]
        public int CustomerId { get; set; }

        public customerVM Customer { get; set; }

        [Required(ErrorMessage = "Please select a product.")]
        public int ProductId { get; set; }

        public ProductVM Product { get; set; }

        [Required(ErrorMessage = "Please enter the quantity.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Please enter the total price.")]
        [Range(0, double.MaxValue, ErrorMessage = "Total price must be a non-negative number.")]
        public decimal TotalPrice { get; set; }

        [Required(ErrorMessage = "Please enter the shipping address.")]
        public string ShippingAddress { get; set; }

        [Required(ErrorMessage = "Please specify the shipment status.")]
        public bool IsShipped { get; set; }

        [Required(ErrorMessage = "Please enter the payment method.")]
        public string PaymentMethod { get; set; }

      

        public List<customerVM> Customers { get; set; }
        public List<ProductVM> Products { get; set; }


        public orderVM()
        {
        }

        public orderVM(orderDTO orderDto)
        {
            Id = orderDto.Id;
            OrderDate = orderDto.OrderDate;
            CustomerId = orderDto.CustomerId;
            ProductId = orderDto.ProductId;
            Quantity = orderDto.Quantity;
            TotalPrice = orderDto.TotalPrice;
            ShippingAddress = orderDto.ShippingAddress;
            IsShipped = orderDto.IsShipped;
            PaymentMethod = orderDto.PaymentMethod;

            Customer = new customerVM(orderDto.Customers);
            Product = new ProductVM(orderDto.Product);
        }

        public void SetCustomers(List<customerVM> customers)
        {
            Customers = customers;
        }

        public void SetProducts(List<ProductVM> products)
        {
            Products = products;
        }
    }
}
