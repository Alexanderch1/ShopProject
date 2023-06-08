using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ApplicationService.DTOs
{
    public class customerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int Age { get; set; }
        public bool IsPremium { get; set; }
        public string Phone { get; set; }

        public bool Validate()
        {
           
            if (string.IsNullOrEmpty(FirstName))
                return false;

            if (string.IsNullOrEmpty(LastName))
                return false;

            if (string.IsNullOrEmpty(Email))
                return false;

         

            return true; 
        }

    }
}
