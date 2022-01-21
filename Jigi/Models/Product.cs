using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Jigi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int CategoryName { get; set; }
        [ForeignKey("CategoryName")]
        public virtual Category GetCategoryName { get; set; }

        public bool IsDelete { get; set; }

        public  DateTime CreatedDate { get; set; }

        [Required]
        public string Description { get; set; }

        public string Image { get; set; }

        [NotMapped]
        public IFormFile Photos { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [NotMapped]
        public string SlideTitle { get; set; }

        public bool IsFeatured { get; set; }



    }
}
