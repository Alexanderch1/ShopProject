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
    public class CustomerManagementService
    {
        
            public List<customerDTO> GetAllCustomers()
            {
                List<customerDTO> customersDto = new List<customerDTO>();

                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    foreach (var item in unitOfWork.CustomersRepository.Get())
                    {
                        customersDto.Add(new customerDTO
                        {
                            Id = item.Id,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Email = item.Email,
                            Address = item.Address,
                            City = item.City,
                            Country = item.Country,
                            RegistrationDate = item.RegistrationDate,
                            Age = item.Age,
                            IsPremium = item.IsPremium,
                            Phone = item.Phone
                        });
                    }
                }

                return customersDto;
            }

            public customerDTO GetCustomerById(int id)
            {
            customerDTO customerDto = new customerDTO();

                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Customers customer = unitOfWork.CustomersRepository.GetByID(id);
                    if (customer != null)
                    {
                        customerDto = new customerDTO
                        {
                            Id = customer.Id,
                            FirstName = customer.FirstName,
                            LastName = customer.LastName,
                            Email = customer.Email,
                            Address = customer.Address,
                            City = customer.City,
                            Country = customer.Country,
                            RegistrationDate = customer.RegistrationDate,
                            Age = customer.Age,
                            IsPremium = customer.IsPremium,
                            Phone = customer.Phone
                        };
                    }
                }

                return customerDto;
            }

            public bool CreateCustomer(customerDTO customerDto)
            {
                Customers customer = new Customers
                {
                    FirstName = customerDto.FirstName,
                    LastName = customerDto.LastName,
                    Email = customerDto.Email,
                    Address = customerDto.Address,
                    City = customerDto.City,
                    Country = customerDto.Country,
                    RegistrationDate = customerDto.RegistrationDate,
                    Age = customerDto.Age,
                    IsPremium = customerDto.IsPremium,
                    Phone = customerDto.Phone
                };

                try
                {
                    using (UnitOfWork unitOfWork = new UnitOfWork())
                    {
                        unitOfWork.CustomersRepository.Insert(customer);
                        unitOfWork.Save();
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public bool UpdateCustomer(customerDTO customerDto)
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Customers customer = unitOfWork.CustomersRepository.GetByID(customerDto.Id);
                    if (customer != null)
                    {
                        customer.FirstName = customerDto.FirstName;
                        customer.LastName = customerDto.LastName;
                        customer.Email = customerDto.Email;
                        customer.Address = customerDto.Address;
                        customer.City = customerDto.City;
                        customer.Country = customerDto.Country;
                        customer.RegistrationDate = customerDto.RegistrationDate;
                        customer.Age = customerDto.Age;
                        customer.IsPremium = customerDto.IsPremium;
                        customer.Phone = customerDto.Phone;

                        unitOfWork.CustomersRepository.Update(customer);
                        unitOfWork.Save();

                        return true;
                    }
                }

                return false;
            }

            public bool DeleteCustomer(int id)
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Customers customer = unitOfWork.CustomersRepository.GetByID(id);
                    if (customer != null)
                    {
                        unitOfWork.CustomersRepository.Delete(customer);
                        unitOfWork.Save();

                        return true;
                    }
                }

                return false;
            }
        }

    }

