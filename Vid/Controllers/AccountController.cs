using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vid.Entities;
using Vid.Models;
using Vid.Utility;
using Vid.ViewModel.AccountVM;

namespace Vid.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _web;
        private readonly ApplicationDbContext _context;

        public AccountController(
           UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext,
           RoleManager<IdentityRole> roleManager, IWebHostEnvironment web, SignInManager<ApplicationUser> signInManager)
        {

            _context = applicationDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _web = web;
        }
 

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {

            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Email);

                    await _userManager.UpdateAsync(user);

                    HttpContext.Session.SetString("ssuserName", user.FirstName);
                    //var userName = HttpContext.Session.GetString("ssuserName");

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid login attempt");
            }
            return View(model);
        }


        public IActionResult Login()
        {

            return View();
        }
        public async Task<IActionResult> Register()
        {
            if (!_roleManager.RoleExistsAsync(Helper.Admin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(Helper.Admin));
                await _roleManager.CreateAsync(new IdentityRole(Helper.User));

            }
            return View();
        }
        [AllowAnonymous]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registrationModel)
        {
            registrationModel.RegistrationInValid = "true";

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = registrationModel.Email,
                    Email = registrationModel.Email,
                    PhoneNumber = registrationModel.PhoneNumber,
                    FirstName = registrationModel.FirstName,
                    LastName = registrationModel.LastName,
                    Address1 = registrationModel.Address1,
                    Address2 = registrationModel.Address2,
                    PostCode = registrationModel.PostCode,
                    Role = registrationModel.RoleName
                };


                var result = await _userManager.CreateAsync(user, registrationModel.Password);

                if (result.Succeeded)
                {
                    registrationModel.RegistrationInValid = "";

                    await _userManager.AddToRoleAsync(user, registrationModel.RoleName);
                    if (!User.IsInRole(Helper.Admin))
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        if (registrationModel.CategoryId != 0)
                        {
                            await AddCategoryToUser(user.Id, registrationModel.CategoryId);

                        }

                        return PartialView("_UserRegistrationPartial", registrationModel);
                    }
                    else
                    {
                        TempData["Admin"] = user.FirstName;
                    }
                    return RedirectToAction("Index", "Home");
                }
                AddErrorsToModelState(result);


            }
            return PartialView("_UserRegistrationPartial", registrationModel);

        }
        [AllowAnonymous]
        public async Task<bool> UserNameExists(string userName)
        {
            bool userNameExists = await _context.Users.AnyAsync(u => u.UserName.ToUpper() == userName.ToUpper());

            if (userNameExists)
                return true;

            return false;

        }

        private void AddErrorsToModelState(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        }

        private async Task AddCategoryToUser(string userId, int categoryId)
        {
            UserCategory userCategory = new UserCategory();
            userCategory.CategoryId = categoryId;
            userCategory.UserId = userId;
            _context.UserCategory.Add(userCategory);
            await _context.SaveChangesAsync();
        }
    }
}
