using Productmanagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Productmanagement.Services
{
    public interface ICategoryService
    {
        //Task<List<Category>> GetCategories();

        List<Category> GetCategories();
        Task<Category> GetCategoryById(int categoryId);
    }
}
