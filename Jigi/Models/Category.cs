using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Jigi.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="CategoryName Required")]
        [StringLength(100,ErrorMessage ="Minimum Of 3 and Maximum of 100")]
        public string CategoryName { get; set; }
        public bool IsDelete { get; set; }
    }
}
