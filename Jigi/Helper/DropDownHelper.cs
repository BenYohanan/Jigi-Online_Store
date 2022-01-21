using Jigi.Data;
using Jigi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jigi.Helper
{
    public class DropDownHelper
    {
        private readonly ApplicationDbContext _database;

        public DropDownHelper(ApplicationDbContext database)
        {
            _database = database;
        }

        public List<Category> GetCategories()
        {
            var allCategoriesInMydb = _database.Categories.Where(c => c.Id != 0).ToList();
            allCategoriesInMydb.Insert(0, new Category { Id = 0, CategoryName = "Selete Category" });
            return allCategoriesInMydb;
        }

        public List<Gender> GetGenders()
        {
            var allGendersIndb = _database.Genders.Where(g => !g.Deleted && g.IsActive).ToList();
            allGendersIndb.Insert(0, new Gender { Id = 0, Name = "Selete Gender" });
            return allGendersIndb;
        }
    }
}
