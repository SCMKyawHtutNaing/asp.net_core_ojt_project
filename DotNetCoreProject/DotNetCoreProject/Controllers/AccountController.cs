using DotNetCoreProject.BLL.Services.IServices;
using DotNetCoreProject.DTO;
using DotNetCoreProject.Entity.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreProject.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly DBContext _dbContext;

        public AccountController(IUserService userService, UserManager<AspNetUser> userManager, SignInManager<AspNetUser> signInManager, RoleManager<IdentityRole> roleManager, IEmailSender emailSender, DBContext dbContext)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //await _roleManager.CreateAsync(new IdentityRole("ADMIN"));
            //await _roleManager.CreateAsync(new IdentityRole("USER"));

            if (ModelState.IsValid)
            {
                var loggedInUser = await _userManager.GetUserAsync(User);

                var user = new AspNetUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Address = "Tamwe",
                    Role = 1,
                    CreatedDate = DateTime.Now,
                    CreatedUserId = loggedInUser.Id
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LogInViewModel loginUser)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginUser.Email);

                var result = await _signInManager.PasswordSignInAsync(user, loginUser.Password, loginUser.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(loginUser);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

        // GET: /Account/ForgotPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    ViewData["errorMessage"] = "Email does not exist.";
                    return View(model);
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { email = user.Email, token = token }, protocol: HttpContext.Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                "<h1>Welcome to Bulletin_Board, " + user.UserName + "</h1><br><p>Your username is: " + user.UserName + ".</p><br><a href='" + callbackUrl + "'>" + callbackUrl + "</a><br><p>Click the link and reset the password.</p><br><p>Thanks for joining and have a great day!</p>");
                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordViewModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordModel);
            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
                return View(resetPasswordModel);
        var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(resetPasswordModel);
            }
            await _dbContext.Entry(user).ReloadAsync();

            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [Authorize, HttpGet]
        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        [Authorize, HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordModel);
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ViewData["errorMessage"] = "User does not exist.";
                return View(resetPasswordModel);
            }

            var pwd_is_valid = await _userManager.CheckPasswordAsync(user, resetPasswordModel.CurrentPassword);

            if (!pwd_is_valid)
            {
                ViewData["errorMessage"] = "Current password is wrong!";
                return View(resetPasswordModel);
            }

            var changePassResult = await _userManager.ChangePasswordAsync(user, resetPasswordModel.CurrentPassword, resetPasswordModel.Password);
            if (!changePassResult.Succeeded)
            {
                foreach (var error in changePassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View();
            }

            await _dbContext.Entry(user).ReloadAsync();

            TempData["successMessage"] = "Password successfully changed!";
            return RedirectToAction("Index", "User");
        }

        [Authorize, HttpGet]
        public async Task<IActionResult> Profile()
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            UserViewModel model = _userService.Get(loggedInUser.Id);

            return View(model);
        }

        [Authorize, HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            UserViewModel model = _userService.Get(loggedInUser.Id);
            ProfileViewModel viewModel = new ProfileViewModel();
            viewModel.Id = model.Id;
            viewModel.Name = model.Name;
            viewModel.Email = model.Email;
            viewModel.Type = model.Type;
            viewModel.Address = model.Address;
            viewModel.Phone = model.Phone;
            viewModel.DOB = model.DOB;
            viewModel.ProfileString = model.ProfileString;
            return View(viewModel);
        }

        [Authorize, HttpPost]
        public async Task<IActionResult> EditProfile(ProfileViewModel model, [FromForm] IFormFile? file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByIdAsync(model.Id);

                    if (file != null)
                    {
                        var fileExtension = Path.GetExtension(file.FileName);

                        if (fileExtension != ".png" && fileExtension != ".jpg" && fileExtension != ".jpeg")
                        {
                            ViewData["errorMessage"] = "Please choose a valid image format.";

                            return View(model);
                        }

                        byte[] imageData = null;
                        MemoryStream ms = new MemoryStream();
                        file.CopyTo(ms);
                        imageData = ms.ToArray();

                        user.Profile = imageData;
                    }

                    // Update it with the values from the view model
                    user.UserName = model.Name;
                    user.DOB = model.DOB == null ? user.DOB : Convert.ToDateTime(model.DOB).Date;
                    user.Email = model.Email;
                    user.PhoneNumber = model.Phone;
                    user.Address = model.Address;

                    var updateResult = await _userManager.UpdateAsync(user);

                    if (!updateResult.Succeeded)
                    {
                        foreach (var error in updateResult.Errors)
                        {
                            ModelState.TryAddModelError(error.Code, error.Description);
                        }
                        return View(model);
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    TempData["successMessage"] = "Profile successfully updated!";
                    return RedirectToAction(nameof(Profile));
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception e)
            {
                ViewData["errorMessage"] = e.Message;
                return View(model);
            }
        }
    }
}
