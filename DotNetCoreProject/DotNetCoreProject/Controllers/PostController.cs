using AutoMapper;
using DotNetCoreProject.BLL.Services.IServices;
using DotNetCoreProject.DTO;
using DotNetCoreProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            PostViewModel model = new PostViewModel();

/*            model.posts = _postService.GetAll();*/

            return View(model);
        }

        [Authorize, HttpGet]
        public async Task<ActionResult<DataTableResponse>> GetPosts(string searchString)
        {
            var posts = _postService.GetAll(searchString);

            return new DataTableResponse
            {
                RecordsTotal = posts.Count(),
                RecordsFiltered = 10,
                Data = posts.ToArray()
            };

        }

        // GET: PostController/Details/5
/*        public ActionResult Details(int id)
        {
            PostViewModel model = _postService.Get(id);

            return PartialView("DetailsPartial", model);
        }*/

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


    }
}