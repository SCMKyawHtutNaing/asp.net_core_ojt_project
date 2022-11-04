using AutoMapper;
using DotNetCoreProject.BLL.Services;
using DotNetCoreProject.BLL.Services.IServices;
using DotNetCoreProject.DTO;
using DotNetCoreProject.Entity.DataContext;
using DotNetCoreProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreProject.Controllers
{
    public class UserController : Controller
    {

        private IUserService _userService;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(IUserService userService, UserManager<AspNetUser> userManager, SignInManager<AspNetUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize, HttpGet]
        public async Task<ActionResult<DataTableResponse>> GetUsers(string nameSearchString, string emailSearchString, string fromSearchString, string toSearchString)
        {
            var users = _userService.GetAll(nameSearchString, emailSearchString, fromSearchString, toSearchString);

            HttpContext.Session.SetComplexData("sessionUserList", users);

            return new DataTableResponse
            {
                RecordsTotal = users.Count(),
                RecordsFiltered = 10,
                Data = users.ToArray()
            };
        }

        [Authorize, HttpGet]
        public async Task<UserViewModel> Details(string id)
        {
            UserViewModel model = _userService.Get(id);

            return model;
        }

        [Authorize, HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model, [FromForm] IFormFile file)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var loggedinUser = await _userManager.GetUserAsync(User);

                    byte[] imageData = null;
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    imageData = ms.ToArray();

                    var user = new AspNetUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        Address = model.Address,
                        Role = 1,
                        CreatedDate = DateTime.Now,
                        CreatedUserId = loggedinUser.Id,
                        Profile = imageData
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var defaultrole = _roleManager.FindByNameAsync("ADMIN").Result;

                        await _userManager.AddToRoleAsync(user, defaultrole.Name);

                        await _signInManager.SignInAsync(user, isPersistent: false);

                        return RedirectToAction("index", "Home");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

                }
                return View(model);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
