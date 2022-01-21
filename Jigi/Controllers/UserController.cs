using Jigi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Jigi.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _database;
     

        public UserController(ApplicationDbContext database)
        {
            _database = database;
           
        }

        //SEARCH PRODUCT BY PRODUCT NAME
        public async Task<IActionResult> SearchProduct(string searchString)
        {
            var products = from product in _database.Products
                           select product;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.ProductName!.Contains(searchString));
            }

            return View(await products.ToListAsync());
        }
    }
}

       