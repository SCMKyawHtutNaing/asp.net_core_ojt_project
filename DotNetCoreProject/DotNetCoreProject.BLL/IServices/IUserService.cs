using DotNetCoreProject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreProject.BLL.Services.IServices
{
    public interface IUserService
    {
        List<UserViewModel> GetAll(string searchString);
        UserViewModel Get(string id);
    }
}
