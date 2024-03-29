﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Jigi.Models
{
    public class SlideImage
    {
        [Key]
        public int Id { get; set; }
        public string SlideTitle { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public IFormFile Photos { get; set; }
    }
}
