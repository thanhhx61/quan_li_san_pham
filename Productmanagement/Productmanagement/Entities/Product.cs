using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Productmanagement.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(500)]
        public string ProductName { get; set; }
        [Required]
        public string Producer { get; set; }
        [Required]
        [MaxLength(300)]
        public string Photo { get; set; }
        [Required]
        public DateTime Year { get; set; }
        [Required]
        public double Price { get; set; }
        public int Amout { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
