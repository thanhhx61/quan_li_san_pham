using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Productmanagement.Models.Product
{
    public class CreateProduct
    {
        [Required(ErrorMessage = "The Product name is required")]
        [MaxLength(500)]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "The Prodcutcer is required")]
        public string Producer { get; set; }

        public IFormFile Photo { get; set; }

        public DateTime Year { get; set; }

        [Required(ErrorMessage = "The Product Price is required")]
        public double Price { get; set; }

        public int Amout { get; set; }
        public string Description { get; set; }
       
        [Required(ErrorMessage = "The book name is required")]
        public int CategoryId { get; set; }
 
    }
}
