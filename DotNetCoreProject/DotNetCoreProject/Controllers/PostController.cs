using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration;
using DocumentFormat.OpenXml.Office2010.Excel;
using DotNetCoreProject.BLL.Services.IServices;
using DotNetCoreProject.DTO;
using DotNetCoreProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Formats.Asn1;
using System.Globalization;
using System.Text;

namespace DotNetCoreProject.Controllers
{
    public class PostController : Controller
    {

        private IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: PostController
        [Authorize, HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize, HttpGet]
        public async Task<ActionResult<DataTableResponse>> GetPosts(string searchString)
        {
            var posts = _postService.GetAll(searchString);

            HttpContext.Session.SetComplexData("sessionPostList", posts);

            return new DataTableResponse
            {
                RecordsTotal = posts.Count(),
                RecordsFiltered = 10,
                Data = posts.ToArray()
            };

        }

        [Authorize, HttpGet]
        public async Task<PostViewModel> Details(int id)
        {
            PostViewModel model = _postService.Get(id);

            return model;
        }

        // GET: PostController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel model)
        {
            try
            {
                bool success = _postService.Save(model);
                if (success) {
                TempData["successMessage"] = "Post successfully created!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["errorMessage"] = "Post creation failed!";
                return View();
            }
        }

        // POST: PostController/CreateConfirm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateConfirm(PostViewModel model)
        {
            try
            {
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Edit/5
        public ActionResult Edit(int id)
        {
            PostViewModel model = _postService.Get(id);

            return View(model);
        }

        // POST: PostController/CreateConfirm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirm(PostViewModel model)
        {
            try
            {
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostViewModel model)
        {
            try
            {
                bool success = _postService.Update(model);
                if (success)
                {
                    TempData["successMessage"] = "Post successfully edited!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["errorMessage"] = "Post edition failed!";
                return View();
            }
        }

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            bool success = _postService.Delete(id);

            if (success)
            {
                TempData["successMessage"] = "Post deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public ActionResult Download()
        {
            List<PostViewModel> lst = HttpContext.Session.GetComplexData<List<PostViewModel>>("sessionPostList");

            var cc = new CsvConfiguration(new System.Globalization.CultureInfo("en-US"));
            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(stream: ms, encoding: new UTF8Encoding(true)))
                {
                    using (var cw = new CsvWriter(sw, cc))
                    {
                        cw.WriteRecords(lst);
                    }
                    return File(ms.ToArray(), "text/csv", $"export_{DateTime.UtcNow.Ticks}.csv");
                }
            }
        }

        [Authorize, HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public ActionResult Upload([FromForm] IFormFile file)
        {
            if (file == null) {
                ViewData["errorMessage"] = "Please choose a file.";

                return View();
            }

            var fileExtension = Path.GetExtension(file.FileName);

            if (fileExtension != ".csv") {
                ViewData["errorMessage"] = "Please choose a csv format.";

                return View();
            }

            try
            {
                var fileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", fileName);
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    file.CopyTo(fs);
                }
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<PostCSVModel>();

                    foreach (var record in records)
                    {
                        if (string.IsNullOrWhiteSpace(record.Title) || string.IsNullOrWhiteSpace(record.Description))
                        {
                            break;
                        }
                        PostViewModel post;
                        post = _postService.Get(record.Title);

                        if (post == null)
                        {
                            post = new PostViewModel();
                        }

                        post.Title = record.Title;
                        post.Description = record.Description;
                        post.Status = bool.Parse(record.Status.ToString());

                        if (post.Id <= 0)
                            _postService.Save(post);
                        else
                            _postService.Update(post);
                    }
                }

                TempData["successMessage"] = "Post CSV successfully uploaded!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewData["errorMessage"] = "Post upload csv must have 3 columns: Title, Description and Status.";

                return View();
            }
        }

    }

    public static class SessionExtensions
    {
        public static T GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetComplexData(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}