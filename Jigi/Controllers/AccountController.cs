using Jigi.Data;
using Jigi.Helper;
using Jigi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace Jigi.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _database;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DropDownHelper _dropDownHelper;


        public AccountController(ApplicationDbContext database, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, DropDownHelper dropDownHelper)
        {
            _database = database;
            _userManager = userManager;
            _signInManager = signInManager;
            _dropDownHelper = dropDownHelper;
        }


        //REGISTER GET ACTION
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.GenderList = _dropDownHelper.GetGenders();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Register(ApplicationUser applicationUser)
        {
            ViewBag.GenderList = _dropDownHelper.GetGenders();
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    { 
                        UserName  = applicationUser.Email,
                        Email = applicationUser.Email,
                        FullName = applicationUser.FullName,
                        PhoneNumber = applicationUser.PhoneNumber,
                        Gender = applicationUser.Gender,
                        Password = applicationUser.Password,
                        ConfirmPassword = applicationUser.ConfirmPassword
                    };

                    var result = await _userManager.CreateAsync(user, applicationUser.Password);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: true);
                        //await _database.AddAsync(user);
                        //await _database.SaveChangesAsync();
                        return RedirectToAction("Index", "Home");
                    }
                }  
                return View(applicationUser);
            }
            catch (Exception )
            {
                throw ;
            }
        }


        //GET ACTION FOR USER LOGIN
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //POST ACTION FOR USER LOGIN
        [HttpPost]
        public async Task<IActionResult> Login(ApplicationUser applicationUser, string returnUrl)
        {
            try
            {
                if (applicationUser.Email != null && applicationUser.Password != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(applicationUser.Email, applicationUser.Password, applicationUser.RememberPassword, true);
                    if (result.Succeeded)
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                            //var UserDetails = await _userManager.Users.Where(u => u.Email == applicationUser.Email && u.Password == applicationUser.Password).FirstOrDefaultAsync();
                        }
                }
                return View(applicationUser);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //CHECK FOR EMAIL
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in Use");
            }
        }


        //LOGOUT Post Action
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        //ACCESS DENIED
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
