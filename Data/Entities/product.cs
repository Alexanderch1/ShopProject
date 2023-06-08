using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{

        public class product : BaseEntity
        {
        [Required]
        [MinLength(1), MaxLength(50)]
         public string Name { get; set; }
            public decimal Price { get; set; }
        [Required]
        [MinLength(1), MaxLength(400)]
        public string Description { get; set; }
        [Required]
        [MinLength(1), MaxLength(55)]
        public string Category { get; set; }
            public DateTime ReleaseDate { get; set; }
            public int UnitsInStock { get; set; }
            public bool IsAvailable { get; set; }

            // Relationships
            public List<Order> Orders { get; set; }
        }

        }





