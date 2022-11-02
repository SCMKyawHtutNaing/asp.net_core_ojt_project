using DotNetCoreProject.DAL.IRepositories;
using DotNetCoreProject.DTO;
using DotNetCoreProject.Entity.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreProject.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext _context;
        public UserRepository(DBContext context)
        {
            _context = context;
        }

        public List<UserViewModel> GetAll(string searchString)
        {
            try
            {
                var query = from user in _context.AspNetUsers
                            from createdUser in _context.AspNetUsers
                                .Where(u => u.Id == user.CreatedUserId).DefaultIfEmpty()
                            from updatedUser in _context.AspNetUsers
                                .Where(u => u.Id == user.UpdatedUserId).DefaultIfEmpty()
                            select new UserViewModel
                            {
                                Id = user.Id,
                                Name = user.UserName,
                                Email = user.Email,
                                CreatedUser = createdUser.UserName,
                                CreatedDate = user.CreatedDate.ToString("yyyy/MM/dd"),
                                UpdatedUser = updatedUser.UserName != null ? updatedUser.UserName : "",
                                UpdatedDate = user.UpdatedDate != null ? user.UpdatedDate.Value.ToString("yyyy/MM/dd") : "",
                                Type = user.Role == 0 ? "Admin" : "User",
                                Address = user.Address,
                                Phone = user.PhoneNumber,
                                DOB = user.DOB != null ? user.DOB.Value.ToString("yyyy/MM/dd") : "",
                            };

                if (!String.IsNullOrEmpty(searchString))
                {
                    query = query.Where(p => p.Name!.Contains(searchString) || p.Name!.Contains(searchString));
                }

                return query.ToList();
            }
            catch (Exception e) {
                return new List<UserViewModel> { new UserViewModel() };
            }
        }

        /*public PostViewModel Get(int id)
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
        }*/
    }
}
