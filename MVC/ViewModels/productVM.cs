using ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime ReleaseDate { get; set; }=DateTime.Now;
        public int UnitsInStock { get; set; }
        public bool IsAvailable { get; set; }

        public ProductVM() { }

        public ProductVM(productDTO productDto)
        {
            Id = productDto.Id;
            Name = productDto.Name;
            Price = productDto.Price;
            Description = productDto.Description;
            Category = productDto.Category;
            ReleaseDate = productDto.ReleaseDate;
            UnitsInStock = productDto.UnitsInStock;
            IsAvailable = productDto.IsAvailable;
        }
    }

}