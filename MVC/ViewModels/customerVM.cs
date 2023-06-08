using ApplicationService.DTOs;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class customerVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the customer's first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter the customer's last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter the customer's email.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter the city.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter the country.")]
        public string Country { get; set; }

        [Display(Name = "Registration Date")]
        [Required(ErrorMessage = "Please enter the registration date.")]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [Range(0, int.MaxValue, ErrorMessage = "Age must be a non-negative number.")]
        public int Age { get; set; }

        [Display(Name = "Is Premium")]
        public bool IsPremium { get; set; }

        [Required(ErrorMessage = "Please enter the phone number.")]
        public string Phone { get; set; }

        public customerVM()
        {
        }

        public customerVM(customerDTO customerDto)
        {
            Id = customerDto.Id;
            FirstName = customerDto.FirstName;
            LastName = customerDto.LastName;
            Email = customerDto.Email;
            Address = customerDto.Address;
            City = customerDto.City;
            Country = customerDto.Country;
            RegistrationDate = customerDto.RegistrationDate;
            Age = customerDto.Age;
            IsPremium = customerDto.IsPremium;
            Phone = customerDto.Phone;
        }

        public static explicit operator customerDTO(customerVM customerVm)
        {
            return new customerDTO
            {
                Id = customerVm.Id,
                FirstName = customerVm.FirstName,
                LastName = customerVm.LastName,
                Email = customerVm.Email,
                Address = customerVm.Address,
                City = customerVm.City,
                Country = customerVm.Country,
                RegistrationDate = customerVm.RegistrationDate,
                Age = customerVm.Age,
                IsPremium = customerVm.IsPremium,
                Phone = customerVm.Phone
            };
        }
    }
}
