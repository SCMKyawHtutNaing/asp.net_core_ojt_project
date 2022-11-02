using DotNetCoreProject.BLL.Services.IServices;
using DotNetCoreProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreProject.Controllers
{
    public class UserController : Controller
    {

        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize, HttpGet]
        public async Task<ActionResult<DataTableResponse>> GetUsers(string searchString)
        {
            var users = _userService.GetAll(searchString);

            HttpContext.Session.SetComplexData("sessionUserList", users);

            return new DataTableResponse
            {
                RecordsTotal = users.Count(),
                RecordsFiltered = 10,
                Data = users.ToArray()
            };

        }
    }
}
