using Data.Context;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class UnitOfWork : IDisposable
    {
        private gamingShopDbContext context = new gamingShopDbContext();
        private GenericRepository<product> productRepository;
        private GenericRepository<Order> orderRepository;
        private GenericRepository<Customers> customersRepository;

        public GenericRepository<product> ProductRepository
        {
            get
            {

                if (this.productRepository == null)
                {
                    this.productRepository = new GenericRepository<product>(context);
                }
                return productRepository;
            }
        }

        public GenericRepository<Order> OrderRepository
        {
            get
            {
                if (this.orderRepository == null)
                {
                    this.orderRepository = new GenericRepository<Order>(context);
                }
                return orderRepository;
            }
        }
        public GenericRepository<Customers> CustomersRepository
        {
            get
            {
                if (this.customersRepository == null)
                {
                    this.customersRepository = new GenericRepository<Customers>(context);
                }
                return customersRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
