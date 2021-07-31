using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Productmanagement.Models.Product
{
    public class ModifyProduct
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "The Product name is required")]
        [MaxLength(500)]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "The Prodcutcer is required")]
        public string Producer { get; set; }
       
        [Required(ErrorMessage = "The Year is required")]
        [Range(minimum: 1999, maximum: 2021)]
        public DateTime Year { get; set; }
        [Required(ErrorMessage = "The Product Price is required")]
        public double Price { get; set; }
        public int Amout { get; set; }
        public IFormFile Photo { get; set; }
        public string ExistPhoto { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "The Id Category is required")]
        public int CategoryId { get; set; }
    }
}
