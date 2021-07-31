using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Productmanagement.Entities;
using Productmanagement.Models.Product;
using Productmanagement.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Productmanagement.Controllers
{
    public class ProductController : Controller
    {
        private static int categoryId;
        private static string categoryName;
        private readonly IProductService productService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ICategoryService categoryService;

        public ProductController(ICategoryService categoryService,
                            IWebHostEnvironment webHostEnvironment,
                            IProductService productService)
        {
            this.categoryService = categoryService;
            this.webHostEnvironment = webHostEnvironment;
            this.productService = productService;
        }
        [Route("Product/Index/{catId=1}")]
        public async Task<IActionResult> Index(int catId)
        {
            categoryId = catId;
            var category = await categoryService.GetCategoryById(catId);
            categoryName = category.CategoryName;
            return View(category);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CategoryName = categoryName;
            ViewBag.CategoryId = categoryId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProduct createProduct)
        {
            if (ModelState.IsValid)
            {
                string filename = "no-photo.jpg";
                if (createProduct.Photo != null)
                {
                    string folderPath = Path.Combine(webHostEnvironment.ContentRootPath, @"wwwroot\images\");
                    filename = $"{DateTime.Now.ToString("ddMMyyyyhhmmss")}_{createProduct.Photo.FileName}";
                    string fullpath = Path.Combine(folderPath, filename);
                    using (var file = new FileStream(fullpath, FileMode.Create))
                    {
                        createProduct.Photo.CopyTo(file);
                    }
                }

                var newproduct = new Product()
                {
                    Photo = $"/images/{filename}",
                    ProductName = createProduct.ProductName,
                    Description = createProduct.Description,
                    Producer = createProduct.Producer,
                    CategoryId = categoryId,
                    IsDeleted = false,
                    Price = createProduct.Price,
                    Year = createProduct.Year,
                    Amout = createProduct.Amout
                    
                };
                await productService.Create(newproduct);
                return RedirectToAction("Index", "Product", new { catId = categoryId });
            }
            ViewBag.CategoryName = categoryName;
            ViewBag.CategoryId = categoryId;
            return View();
        }

        [HttpGet]
        [Route("/Product/Modify/{ProductId}")]
        public async Task<IActionResult> Modify(int productId)
        {
            ViewBag.CategoryName = categoryName;
            ViewBag.CategoryId = categoryId;
            var product = await productService.GetProductById(productId);
            var modifyProduct = new ModifyProduct()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Year = product.Year,
                CategoryId = product.CategoryId,
                Description = product.Description,
                ExistPhoto = product.Photo,
                Price = product.Price,
                Amout = product.Amout

            };
            return View(modifyProduct);
        }
        [HttpPost]
        public async Task<IActionResult> Modify(ModifyProduct modifyProduct)
        {
            if (ModelState.IsValid)
            {
                var product = await productService.GetProductById(modifyProduct.ProductId);
                if (product != null)
                {
                    string filename = product.Photo;
                    if (modifyProduct.Photo != null)
                    {
                        //Delete old photo
                        var oldFileName = filename.Split("/")[2];
                        if (string.Compare(oldFileName, "no-photo.jpg") != 0)
                        {
                            System.IO.File.Delete(Path.Combine(webHostEnvironment.ContentRootPath, @"wwwroot\images\", oldFileName));
                        }

                        string folderPath = Path.Combine(webHostEnvironment.ContentRootPath, @"wwwroot\images\");
                        filename = $"{DateTime.Now.ToString("ddMMyyyyhhmmss")}_{modifyProduct.Photo.FileName}";
                        string fullpath = Path.Combine(folderPath, filename);
                        using (var file = new FileStream(fullpath, FileMode.Create))
                        {
                            modifyProduct.Photo.CopyTo(file);
                        }
                    }

                    product.Photo = modifyProduct.Photo != null ? $"/images/{filename}" : filename;
                    product.ProductName = modifyProduct.ProductName;
                    product.Description = modifyProduct.Description;
                    product.Producer = modifyProduct.Producer;
                    product.Year = modifyProduct.Year;
                    product.CategoryId = categoryId;
                    product.Price = modifyProduct.Price;          
                    product.ProductId = modifyProduct.ProductId;
                    product.Amout = modifyProduct.Amout;

                    await productService.Modify(product);
                    return RedirectToAction("Index", "Product", new { catId = categoryId });
                }

            }
            ViewBag.CategoryName = categoryName;
            ViewBag.CategoryId = categoryId;
            return View(modifyProduct);
        }
        [HttpGet]
        [Route("/Product/View/{ProductId}")]
        public async Task<IActionResult> View(int prodcutId)
        {
            var product = await productService.GetProductById(prodcutId);
            var viewProduct = new ViewProduct()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Producer = product.Producer,
                CategoryId = product.CategoryId,
                Description = product.Description,
                ExistPhoto = product.Photo,
                Price = product.Price,               
                Category = product.Category,
                Amout = product.Amout
            };
            return View(viewProduct);
        }

        [HttpGet]
        [Route("/Product/Remove/{productId}")]
        public async Task<IActionResult> Remove(int productId)
        {
            await productService.Remove(productId);
            return RedirectToAction("Index", "Product", new { catId = categoryId });
        }

        [HttpGet]
        [Route("/Product/Restore/{productId}")]
        public async Task<IActionResult> Restore(int productId)
        {
            await productService.Restore(productId);
            return RedirectToAction("Index", "Product", new { catId = categoryId });
        }
    }
}
