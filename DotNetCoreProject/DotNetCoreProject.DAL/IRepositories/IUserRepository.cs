using DotNetCoreProject.DTO;
using DotNetCoreProject.Entity.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreProject.DAL.IRepositories
{
    public interface IUserRepository
    {
        List<UserViewModel> GetAll(string searchString);
/*        UserViewModel Get(int id);
        UserViewModel Get(string title);
        AspNetUser GetEntity(int id);
        bool Save(AspNetUser obj);
        bool Update(AspNetUser obj);*/
    }
}
