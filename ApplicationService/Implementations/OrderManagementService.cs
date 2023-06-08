using ApplicationService.DTOs;
using Data.Entities;
using Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Implementations
{
    public class OrderManagementService
    {
        public List<orderDTO> GetAllOrders()
        {
            List<orderDTO> ordersDto = new List<orderDTO>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var orders = unitOfWork.OrderRepository.Get(includeProperties: "Customers,Product");

                if (orders != null)
                {
                    foreach (var order in orders)
                    {
                        orderDTO orderDto = new orderDTO
                        {
                            Id = order.Id,
                            OrderDate = order.OrderDate,
                            CustomerId = order.CustomerId,
                            Customers = order.Customers != null ? new customerDTO
                            {
                                Id = order.Customers.Id,
                                FirstName = order.Customers.FirstName,
                                LastName = order.Customers.LastName,
                            } : null,
                            ProductId = order.ProductId,
                            Product = order.Product != null ? new productDTO
                            {
                                Id = order.Product.Id,
                                Name = order.Product.Name,
                                Price = order.Product.Price,
                            } : null,
                            Quantity = order.Quantity,
                            TotalPrice = order.TotalPrice,
                            ShippingAddress = order.ShippingAddress,
                            IsShipped = order.IsShipped,
                            PaymentMethod = order.PaymentMethod
                        };

                        if (orderDto.Customers != null && orderDto.Product != null)
                        {
                            ordersDto.Add(orderDto);
                        }
                    }
                }
            }

            return ordersDto;
        }


        public orderDTO GetOrderById(int id)
        {
            orderDTO orderDto = new orderDTO();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Order order = unitOfWork.OrderRepository.GetByID(id);
                if (order != null)
                {
                    Customers customer = unitOfWork.CustomersRepository.GetByID(order.CustomerId);
                    product product = unitOfWork.ProductRepository.GetByID(order.ProductId);

                    orderDto = new orderDTO
                    {
                        Id = order.Id,
                        OrderDate = order.OrderDate,
                        CustomerId = order.CustomerId,
                        Customers = new customerDTO
                        {
                            Id = customer.Id,
                            FirstName = customer.FirstName,
                            LastName = customer.LastName,
                        },
                        ProductId = order.ProductId,
                        Product = new productDTO
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Price = product.Price,
                        },
                        Quantity = order.Quantity,
                        TotalPrice = order.TotalPrice,
                        ShippingAddress = order.ShippingAddress,
                        IsShipped = order.IsShipped,
                        PaymentMethod = order.PaymentMethod
                    };
                }
            }

            return orderDto;
        }


        public bool CreateOrder(orderDTO orderDto)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    
                    Customers customer = unitOfWork.CustomersRepository.GetByID(orderDto.CustomerId);

                 
                    product product = unitOfWork.ProductRepository.GetByID(orderDto.ProductId);

                    
                    Order order = new Order
                    {
                        OrderDate = orderDto.OrderDate,
                        Customers = customer,
                        Product = product,
                        Quantity = orderDto.Quantity,
                        TotalPrice = orderDto.TotalPrice,
                        ShippingAddress = orderDto.ShippingAddress,
                        IsShipped = orderDto.IsShipped,
                        PaymentMethod = orderDto.PaymentMethod
                    };

                    unitOfWork.OrderRepository.Insert(order);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool UpdateOrder(orderDTO orderDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Order order = unitOfWork.OrderRepository.GetByID(orderDto.Id);
                if (order != null)
                {
                    order.OrderDate = orderDto.OrderDate;
                    order.CustomerId = orderDto.CustomerId;
                    order.ProductId = orderDto.ProductId;
                    order.Quantity = orderDto.Quantity;
                    order.TotalPrice = orderDto.TotalPrice;
                    order.ShippingAddress = orderDto.ShippingAddress;
                    order.IsShipped = orderDto.IsShipped;
                    order.PaymentMethod = orderDto.PaymentMethod;

                    unitOfWork.OrderRepository.Update(order);
                    unitOfWork.Save();

                    return true;
                }
            }

            return false;
        }

        public bool DeleteOrder(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Order order = unitOfWork.OrderRepository.GetByID(id);
                if (order != null)
                {
                    unitOfWork.OrderRepository.Delete(order);
                    unitOfWork.Save();

                    return true;
                }
            }

            return false;
        }



    }
}
