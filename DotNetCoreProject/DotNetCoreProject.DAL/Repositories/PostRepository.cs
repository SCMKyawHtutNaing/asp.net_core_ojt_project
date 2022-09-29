using DotNetCoreProject.DAL.IRepositories;
using DotNetCoreProject.DTO;
using DotNetCoreProject.Entity.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreProject.DAL.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly EmployeeDBContext _context;
        public PostRepository(EmployeeDBContext context)
        {
            _context = context;
        }

        public List<PostViewModel> GetAll(string searchString)
        {
            var query = from p in _context.Posts
                             from u in _context.AspNetUsers
                             where p.CreatedUserId == u.Id & p.IsDeleted==false
                             select new PostViewModel
                             {
                                 Id = p.Id,
                                 Title = p.Title,
                                 Description = p.Description,
                                 PostedUser = u.UserName,
                                 PostedDate = p.CreatedDate.ToString("yyyy/MM/dd")
                             };

            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => p.Title!.Contains(searchString) || p.Description!.Contains(searchString));
            }

            return query.ToList();
        }

        public PostViewModel Get(int id)
        {
            var query = (from data in _context.Posts
                         where data.Id == id
                         select new PostViewModel
                         {
                             Id = data.Id,
                             Title = data.Title,
                             Description = data.Description,
                             PostedDate = data.CreatedDate.ToString("yyyy/MM/dd")
                         });

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

        public bool Delete(int id)
        {
            bool success = false;
            try
            {
                Post post = _context.Posts.FirstOrDefault(x => x.Id == id);

                _context.Posts.Remove(post);
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
