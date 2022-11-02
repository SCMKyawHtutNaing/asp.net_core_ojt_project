﻿using DotNetCoreProject.DAL.IRepositories;
using DotNetCoreProject.DTO;
using DotNetCoreProject.Entity.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreProject.DAL.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DBContext _context;
        public PostRepository(DBContext context)
        {
            _context = context;
        }

        public List<PostViewModel> GetAll(string searchString)
        {
            var query = from p in _context.Posts
                        where p.IsDeleted == false
                        join c in _context.AspNetUsers on p.CreatedUserId equals c.Id
                        join u in _context.AspNetUsers on p.UpdatedUserId equals u.Id
                        into rightMatches
                        from u2 in rightMatches.DefaultIfEmpty()
                        select new PostViewModel
                        {
                            Id = p.Id,
                            Title = p.Title,
                            Description = p.Description,
                            CreatedUser = c.UserName,
                            CreatedDate = p.CreatedDate.ToString("yyyy/MM/dd"),
                            UpdatedUser = u2.UserName != null ? u2.UserName : "",
                            UpdatedDate = p.UpdatedDate != null ? p.UpdatedDate.Value.ToString("yyyy/MM/dd") : "",
                            Status = p.Status == 1 ? true : false
                        };

            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => p.Title!.Contains(searchString) || p.Description!.Contains(searchString));
            }

            return query.ToList();
        }

        public PostViewModel Get(int id)
        {
            var query = from p in _context.Posts
                        where p.Id == id & p.IsDeleted == false
                        join c in _context.AspNetUsers on p.CreatedUserId equals c.Id
                        join u in _context.AspNetUsers on p.UpdatedUserId equals u.Id
                        into rightMatches
                        from u2 in rightMatches.DefaultIfEmpty()
                        select new PostViewModel
                        {
                            Id = p.Id,
                            Title = p.Title,
                            Description = p.Description,
                            CreatedUser = c.UserName,
                            CreatedDate = p.CreatedDate.ToString("yyyy/MM/dd"),
                            UpdatedUser = u2.UserName != null ? u2.UserName : "",
                            UpdatedDate = p.UpdatedDate != null ? p.UpdatedDate.Value.ToString("yyyy/MM/dd") : "",
                            Status = p.Status == 1 ? true : false
                        };

            return query.FirstOrDefault();
        }

        public PostViewModel Get(string title)
        {
            var query = from p in _context.Posts
                        where p.Title == title & p.IsDeleted == false
                        join c in _context.AspNetUsers on p.CreatedUserId equals c.Id
                        join u in _context.AspNetUsers on p.UpdatedUserId equals u.Id
                        into rightMatches
                        from u2 in rightMatches.DefaultIfEmpty()
                        select new PostViewModel
                        {
                            Id = p.Id,
                            Title = p.Title,
                            Description = p.Description,
                            CreatedUser = c.UserName,
                            CreatedDate = p.CreatedDate.ToString("yyyy/MM/dd"),
                            UpdatedUser = u2.UserName != null ? u2.UserName : "",
                            UpdatedDate = p.UpdatedDate != null ? p.UpdatedDate.Value.ToString("yyyy/MM/dd") : "",
                            Status = p.Status == 1 ? true : false
                        };

            return query.FirstOrDefault();
        }

        public Post GetEntity(int id)
        {
            var query = (from data in _context.Posts where data.Id == id select data);

            return query.FirstOrDefault();
        }

        public bool Save(Post obj)
        {
            bool success = false;
            try
            {
                _context.Add(obj);
                _context.SaveChanges();

                success = true;

            }
            catch (Exception ex)
            {

            }

            return success;
        }

        public bool Update(Post obj)
        {
            bool success = false;
            try
            {
                _context.Posts.Update(obj);
                _context.SaveChanges();

                success = true;

            }
            catch (Exception ex)
            {

            }

            return success;
        }

    }
}
