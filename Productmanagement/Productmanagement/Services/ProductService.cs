using Microsoft.EntityFrameworkCore;
using Productmanagement.DBContexts;
using Productmanagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Productmanagement.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductShopDBContext context;
        public ProductService(ProductShopDBContext context)
        {
            this.context = context;
        }
        //CREAT
        public async Task<Product> Create(Product createProduct)
        {
            try
            {
                context.Add(createProduct);
                var productId = await context.SaveChangesAsync();
                createProduct.ProductId = productId;
                return createProduct;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await context.products.Include(c => c.Category).FirstOrDefaultAsync(b => b.ProductId == productId);
        }
        //EDIT
        public async Task<Product> Modify(Product product)
        {
            try
            {
                context.Attach(product);
                context.Entry<Product>(product).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //DELETE
        public async Task<Product> Remove(int productId)
        {
            try
            {
                var product = await GetProductById(productId);
                product.IsDeleted = true;
                context.Attach(product);
                context.Entry<Product>(product).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //RESTORE
        public async Task<Product> Restore(int productId)
        {
            try
            {
                var product = await GetProductById(productId);
                product.IsDeleted = false;
                context.Attach(product);
                context.Entry<Product>(product).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
