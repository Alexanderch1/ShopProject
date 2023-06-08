using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Customers : BaseEntity
    {
        [Required]
        [MinLength(1), MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1), MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MinLength(1), MaxLength(200)]
        public string Email { get; set; }
        [Required]
        [MinLength(1), MaxLength(200)]
        public string Address { get; set; }
        [Required]
        [MinLength(1), MaxLength(30)]
        public string City { get; set; }
        [Required]
        [MinLength(1), MaxLength(30)]
        public string Country { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int Age { get; set; }
        public bool IsPremium { get; set; }
        [Required]
        [MinLength(1), MaxLength(12)]
        public string Phone { get; set; }

        // Relationships
        public List<Order> Orders { get; set; }


    }
}
