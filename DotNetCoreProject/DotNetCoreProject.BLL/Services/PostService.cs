using AutoMapper;
using ClosedXML.Excel;
using DotNetCoreProject.BLL.Services.IServices;
using DotNetCoreProject.DAL.IRepositories;
using DotNetCoreProject.DTO;
using DotNetCoreProject.Entity.DataContext;
using System.Data;
using System.Web;

namespace DotNetCoreProject.BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }
        public List<PostViewModel> GetAll(string searchString)
        {
            List<PostViewModel> lst = _postRepository.GetAll(searchString);
            return lst;
        }

        public PostViewModel Get(int id)
        {
            PostViewModel model = _postRepository.Get(id);
            return model;
        }

        public PostViewModel Get(string title)
        {
            PostViewModel model = _postRepository.Get(title);
            return model;
        }

        public bool Save(PostViewModel model)
        {
            Post post = new Post();
            post.Title = model.Title;
            post.Description = model.Description;
            post.Status = 1;
            post.IsActive = true;
            post.IsDeleted = false;
            post.CreatedDate = DateTime.Now;
            post.CreatedUserId = "1";
            bool success = _postRepository.Save(post);
            return success;
        }

        public bool Update(PostViewModel model)
        {
            Post post = _postRepository.GetEntity(model.Id);
            post.Title = model.Title;
            post.Description = model.Description;
            post.Status = model.Status ? 1 : 0;
            post.UpdatedDate = DateTime.Now;
            post.UpdatedUserId = "1";
            bool success = _postRepository.Update(post);
            return success;
        }

        public bool Delete(int id)
        {
            Post post = _postRepository.GetEntity(id);
            post.IsDeleted = true;
            post.DeletedDate = DateTime.Now;
            post.DeletedUserId = "1";
            bool success = _postRepository.Update(post);
            return success;
        }
        public DataTable GetDataTableForDownload(string searchString) {
            List<PostViewModel> lst = _postRepository.GetAll(searchString);

            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Title"), new DataColumn("Description"), new DataColumn("Posted User"), new DataColumn("Posted Date") });

            foreach (var emp in lst)
            {
                dt.Rows.Add(emp.Title, emp.Description, emp.CreatedUser, emp.CreatedDate);
            }

            return dt;
        }
    }
}
