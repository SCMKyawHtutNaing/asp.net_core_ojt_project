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
        public ActionResult CreateConfirm(UserViewModel model, [FromForm] IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var fileExtension = Path.GetExtension(file.FileName);

                    if (fileExtension != ".png" && fileExtension != ".jpg" && fileExtension != ".jpeg")
                    {
                        ViewData["errorMessage"] = "Please choose a valid image format.";

                        return View("Create", model);
                    }

                    byte[] imageData = null;
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    imageData = ms.ToArray();

                    model.ProfileString = "data:image/jpg;base64," + Convert.ToBase64String(imageData);

                    return View(model);

                }
                else {
                    if (file == null)
                    {
                        ViewData["errorMessage"] = "Profile can't be blank.";
                    }

                    return View("Create", model);
                }
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var loggedInUser = await _userManager.GetUserAsync(User);

                    var user = new AspNetUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        Address = model.Address,
                        Role = 1,
                        CreatedDate = DateTime.Now,
                        CreatedUserId = loggedInUser.Id,
                        Profile = model.ProfileBytes,
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var defaultrole = _roleManager.FindByNameAsync("ADMIN").Result;

                        await _userManager.AddToRoleAsync(user, defaultrole.Name);

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        
                        TempData["successMessage"] = "User successfully created!";
                        return RedirectToAction(nameof(Index));
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
