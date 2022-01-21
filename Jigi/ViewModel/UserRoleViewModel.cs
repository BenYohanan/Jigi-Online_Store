﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Jigi.ViewModel
{
    public class UserRoleViewModel
    {
        
        public string UserId { get; set; }
   
        public string UserName { get; set; }
        
        public bool IsSelected { get; set; }
    }
}
