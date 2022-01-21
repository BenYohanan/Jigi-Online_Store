using Jigi.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Jigi.ViewModel
{

    public class ProductViewModel
    {
        [Required]

        public int Id { get; set; }
        public string ProductName { get; set; }

        [Required]
        public int CategoryName { get; set; }
        [ForeignKey("CategoryName")]
        public virtual Category GetCategoryDeatails { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public string Description { get; set; }

        public IFormFile ProductImage { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public bool IsFeatured { get; set; }
    }
}
