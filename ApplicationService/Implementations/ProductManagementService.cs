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
    public class ProductManagementService
    {


        public List<productDTO> Get()
        {
            List<productDTO> productsDto = new List<productDTO>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var item in unitOfWork.ProductRepository.Get())
                {
                    productsDto.Add(new productDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        Description = item.Description,
                        Category = item.Category,
                        ReleaseDate = item.ReleaseDate,
                        UnitsInStock = item.UnitsInStock,
                        IsAvailable = item.IsAvailable
                    });
                }
            }
            return productsDto;
        }

        public productDTO GetProductById(int id)
        {
            productDTO productDto = new productDTO();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                product product = unitOfWork.ProductRepository.GetByID(id);
                if (product != null)
                {
                    productDto = new productDTO
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Description = product.Description,
                        Category = product.Category,
                        ReleaseDate = product.ReleaseDate,
                        UnitsInStock = product.UnitsInStock,
                        IsAvailable = product.IsAvailable
                    };
                }
            }
            return productDto;
        }

        public bool CreateProduct(productDTO productDto)
        {
            product product = new product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                Category = productDto.Category,
                ReleaseDate = productDto.ReleaseDate,
                UnitsInStock = productDto.UnitsInStock,
                IsAvailable = productDto.IsAvailable
            };

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    unitOfWork.ProductRepository.Insert(product);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateProduct(productDTO productDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                product product = unitOfWork.ProductRepository.GetByID(productDto.Id);
                if (product != null)
                {
                    product.Name = productDto.Name;
                    product.Price = productDto.Price;
                    product.Description = productDto.Description;
                    product.Category = productDto.Category;
                    product.ReleaseDate = productDto.ReleaseDate;
                    product.UnitsInStock = productDto.UnitsInStock;
                    product.IsAvailable = productDto.IsAvailable;

                    unitOfWork.ProductRepository.Update(product);
                    unitOfWork.Save();

                    return true;
                }
            }

            return false;
        }

        public bool DeleteProduct(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                product product = unitOfWork.ProductRepository.GetByID(id);
                if (product != null)
                {
                    unitOfWork.ProductRepository.Delete(product);
                    unitOfWork.Save();

                    return true;
                }
            }

            return false;
        }
    }

}
