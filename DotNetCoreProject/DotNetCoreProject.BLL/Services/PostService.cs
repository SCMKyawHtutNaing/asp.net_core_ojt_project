using AutoMapper;
using DotNetCoreProject.BLL.Services.IServices;
using DotNetCoreProject.DAL.IRepositories;
using DotNetCoreProject.DTO;
using DotNetCoreProject.Entity.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<PostViewModel> GetAll()
        {
            List<PostViewModel> lst = _postRepository.GetAll();
            return lst;
        }

        public PostViewModel Get(int id)
        {
            PostViewModel model = _postRepository.Get(id);
            return model;
        }

        public bool Save(PostViewModel model)
        {
            bool success = _postRepository.Save(_mapper.Map<Post>(model));
            return success;
        }

        public bool Update(PostViewModel model)
        {
            bool success = _postRepository.Update(_mapper.Map<Post>(model));
            return success;
        }

        public bool Delete(int id)
        {
            bool success = _postRepository.Delete(id);
            return success;
        }
    }
}
