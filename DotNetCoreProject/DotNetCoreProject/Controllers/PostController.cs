﻿using AutoMapper;
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
        public ActionResult Details(int id)
        {
            PostViewModel model = _postService.Get(id);

            return View(model);
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

                return RedirectToAction(nameof(Index));
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

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostViewModel model)
        {
            try
            {
                bool success = _postService.Update(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            bool success = _postService.Delete(id);


            return RedirectToAction(nameof(Index));
        }


    }
}