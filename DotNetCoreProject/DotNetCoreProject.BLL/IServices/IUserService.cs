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
/*        UserViewModel Get(int id);
        UserViewModel Get(string title);
        bool Save(UserViewModel model);
        bool Update(UserViewModel model);
        bool Delete(int id);*/
    }
}
