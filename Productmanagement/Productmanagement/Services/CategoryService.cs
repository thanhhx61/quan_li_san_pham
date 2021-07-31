using Microsoft.EntityFrameworkCore;
using Productmanagement.DBContexts;
using Productmanagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Productmanagement.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ProductShopDBContext context;
        public CategoryService (ProductShopDBContext context)
        {
            this.context = context;
        }
        public List<Category> GetCategories()
        {
            List<Category> list = new List<Category>();
            //list =  await context.categories.Include(b => b.products).Where(c => c.IsDeleted == false).ToListAsync();
            list = context.categories.ToList();
            return list;


            return context.categories.Include(b => b.products).Where(c => c.IsDeleted == false).ToList();
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await context.categories.Include(b => b.products).FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }
    }
}
