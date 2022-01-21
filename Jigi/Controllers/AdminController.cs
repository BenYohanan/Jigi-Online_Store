using Jigi.Data;
using Jigi.Helper;
using Jigi.Models;
using Jigi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Jigi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _database;
        private readonly IWebHostEnvironment _iWebhostingEnvironment;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DropDownHelper _dropDownHelper;

        public AdminController(ApplicationDbContext database, IWebHostEnvironment iWebhostingEnvironment,
            RoleManager<Role> roleManager, UserManager<ApplicationUser> userManager, DropDownHelper dropDownHelper)
        {
            _database = database;
            _iWebhostingEnvironment = iWebhostingEnvironment;
            _roleManager = roleManager;
            _userManager = userManager;
            _dropDownHelper = dropDownHelper;
        }

        //GET ACTION VIEW FOR ADMIN DASHBOARD
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult BackView()
        {
            return View();
        }

        //GET ACTION FOR CATEGORY
        public IActionResult Categories()
        {
            IEnumerable<Category> categoriesAvaliable = _database.Categories;
            return View(categoriesAvaliable);
        }

        // GET ACTION OF ADD NEW ITEM TO CATEGORIES
        public IActionResult UpdateCategory()
        {
            return View();
        }

        //POST ACTION OF ADD NEW ITEM TO CATEGORIES
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                Category newCategory = new Category
                {
                    CategoryName = category.CategoryName,
                    IsDelete = category.IsDelete

                };
                _database.Categories.Add(newCategory);
                _database.SaveChanges();
                return RedirectToAction("Categories");
            }
            return View();
        }

        //EDIT CATEGORY
        [HttpPost]
        public IActionResult EditCategory(int? categoryId)
        {
            if (categoryId != null || categoryId != 0)
            {
                var categoryList = _database.Categories.Find(categoryId);
                if (categoryList != null)
                {
                    return View(categoryList);
                }
            }
            return View();
        }

        //Delect Category
        [HttpGet]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (categoryId == 0)
            {
                return NotFound("Not Found");
            }
            var getCategoryByCategoryId = _database.Categories
                .Where(d => d.Id == categoryId)
                .FirstOrDefault();
            if (getCategoryByCategoryId.IsDelete == true)
            {
                //error Message == order already deleted
                return View("BackView");
            }
            getCategoryByCategoryId.IsDelete = true;
            _database.Categories.Remove(getCategoryByCategoryId);
            _database.SaveChanges();
            return RedirectToAction("Categories");
        }

        //GET ACTION VIEW FOR PRODUCT
        public IActionResult Product()
        {
            IEnumerable<Product> avaliableProduct = _database.Products;
            return View(avaliableProduct);
        }

        //GET VIEW ACTION FOR PRODUCTEDIT
        public IActionResult ProductEdit(int productId)
        {
            ViewBag.CategoryList =_dropDownHelper.GetCategories();
            if (productId == 0)
            {
                return NotFound("No Such Product");
            }
            var productList = _database.Products.Find(productId);
            if (productList == null)
            {
                return NotFound("Yes Not Found");
            }

            return View(productList);
        }

        //POST ACTION FOR PRODUCT EDIT
        [HttpPost]
        public IActionResult ProductEdit(Product product)
        {
            ViewBag.CategoryList = _dropDownHelper.GetCategories();
            var updatedProduct = _database.Products
                .Where(p => p.Id == product.Id)
                .FirstOrDefault();
            if (product.ProductName != null && product.CategoryName != 0 && product.Quantity != 0)
            {
                if (product.Description != null && product.Price != 0)
                {
                    if (updatedProduct != null)
                    {
                        var productImage = SavePicture(product.Photos);

                        updatedProduct.ProductName = product.ProductName;
                        updatedProduct.CategoryName = product.CategoryName;
                        updatedProduct.IsFeatured = product.IsFeatured;
                        updatedProduct.Description = product.Description;
                        updatedProduct.Image = productImage;
                        updatedProduct.Quantity = product.Quantity;
                        updatedProduct.Price = product.Price;

                        _database.Products.Update(updatedProduct);
                        _database.SaveChanges();
                        return RedirectToAction("Product");
                    }
                }
            }
            return View(product);
        }


        //GET VIEW ACTION FOR PRODUCT ADD
        public IActionResult AddProduct()
        {
            ViewBag.CategoryList = _dropDownHelper.GetCategories();
            return View();
        }

        //SAVE PICTURE TO DATABASE
        public string SavePicture(IFormFile savepicture)
        {
            string uniqueFileName = null;
            if (savepicture.FileName != null)
            {
                string images = Path.Combine(_iWebhostingEnvironment.WebRootPath, "ProductImage");
                string pathString = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductImage");
                if (!Directory.Exists(pathString))
                {
                    Directory.CreateDirectory(pathString);
                }
                uniqueFileName = Guid.NewGuid().ToString() + "_" + savepicture.FileName;
                string filePath = Path.Combine(images, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    savepicture.CopyTo(fileStream);
                }
            }
            return "/ProductImage/" + uniqueFileName;
        }

        //POST ACTION OF ADD NEW ITEM TO PRODUCTS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(Product product)

        {
            ViewBag.CategoryList =_dropDownHelper.GetCategories();
            if (product.ProductName != null && product.CategoryName != 0 && product.Quantity != 0)
            {
                if (product.Description != null && product.Price != 0)
                {
                    var productImage = SavePicture(product.Photos);

                    Product newProduct = new Product
                    {
                        ProductName = product.ProductName,
                        CategoryName = product.CategoryName,
                        IsDelete = false,
                        IsFeatured = product.IsFeatured,
                        CreatedDate = product.CreatedDate,
                        Description = product.Description,
                        Image = productImage,
                        Quantity = product.Quantity,
                        Price = product.Price,
                    };
                    _database.Products.Add(newProduct);
                    _database.SaveChanges();
                    return RedirectToAction("Product");
                }
            }
            return View();
        }

        //Delete Product
        [HttpPost]
        public IActionResult DeleteProduct(int? productId)
        {
            if (productId == 0)
            {
                return NotFound("Not Found");
            }
            var getProductByProductId = _database.Products
                .Where(d => d.Id == productId)
                .FirstOrDefault();

            if (getProductByProductId.IsDelete == true)
            {
                //error Message == order already deleted
                return View("BackView");
            }
            getProductByProductId.IsDelete = true;
            _database.Products.Remove(getProductByProductId);
            _database.SaveChanges();
            return RedirectToAction("Product");
        }

        //GET ACTION FOR SLIDE IMAGE
        [HttpGet]
        public IActionResult SlideImage()
        {
            return View();
        }

        //ADD IMAGE TO SLIDE
        [HttpPost]
        public IActionResult SlideImage(SlideImage slideImage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var image = SavePicture(slideImage.Photos);
                    SlideImage slide = new SlideImage
                    {
                        SlideTitle = slideImage.SlideTitle,
                        Image = image,
                    };
                    _database.slideImages.Add(slide);
                    _database.SaveChanges();
                    return RedirectToAction("");
                }
                return View(slideImage);
            }
            catch (Exception)
            {
                throw; 
            }    
        }


        //GET VIEW OF CREATE ROLE ACTION
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }


        //POST ACTION OF CREATEROLE VIEW 
        [HttpPost]
        public async Task<IActionResult> CreateRole(Role role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var identityRole = new Role
                    {
                        Name = role.Name
                    };

                    IdentityResult result = await _roleManager.CreateAsync(identityRole);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRole", "Admin");
                    }
                }
                return View(role);
            }
            catch (Exception)
            {
                throw;
            }

        }

        //GET ACTION OF LIST ROLE ACTION
        [HttpGet]
        public IActionResult ListRole()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        //EDIT ROLES IN ADMIN PANEL
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }


        //EDIT ROLES POST ACTION
        [HttpPost]
        public async Task<IActionResult> EditRole(Role role)
        {
            try
            {
                var userInThisRole = await _roleManager.FindByIdAsync(role.Id);
                if (userInThisRole != null)
                {
                    userInThisRole.Name = role.Name;
                    var result = await _roleManager.UpdateAsync(userInThisRole);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRole");
                    }
                }
                return View(role);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //EditUsersInRole Get Action
        [HttpGet]
        public async Task<IActionResult> EditUserInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }
            var model = new List<UserRoleViewModel>();
            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        //EditUsersInRole Get Action
        [HttpPost]
        public async Task<IActionResult> EditUserInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                //ViewBag.ErrorMessage = $"Role with Id={roleId} cannot br found";
                return NotFound();
            }
            for(int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else return RedirectToAction("EditRole", new { Id = roleId });
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }

        //LIST USERS ACTION METHOD
        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = _userManager.Users;
            return View(users);
        }

        //DELETE USER
        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
            }
            return View("ListUsers");
        }
    }
}
