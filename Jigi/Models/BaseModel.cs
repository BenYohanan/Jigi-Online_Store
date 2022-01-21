using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Jigi.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string  Name { get; set; }
        public bool  IsActive { get; set; }
        public bool  Deleted { get; set; }
    }
}
