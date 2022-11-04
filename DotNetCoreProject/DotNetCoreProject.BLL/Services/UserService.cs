using AutoMapper;
using DotNetCoreProject.BLL.Services.IServices;
using DotNetCoreProject.DAL.IRepositories;
using DotNetCoreProject.DAL.Repositories;
using DotNetCoreProject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreProject.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public List<UserViewModel> GetAll(string nameSearchString, string emailSearchString, string fromSearchString, string toSearchString)
        {
            List<UserViewModel> lst = _userRepository.GetAll(nameSearchString, emailSearchString, fromSearchString, toSearchString);
            return lst;
        }

        public UserViewModel Get(string id)
        {
            UserViewModel model = _userRepository.Get(id);
            return model;
        }
    }
}
