using Productmanagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Productmanagement.Services
{
    public interface IProductService
    {
        Task<Product> Create(Product createProduct);
        Task<Product> GetProductById(int productId);
        Task<Product> Modify(Product product);
        Task<Product> Remove(int productId);
        Task<Product> Restore(int productId);
    }
}
