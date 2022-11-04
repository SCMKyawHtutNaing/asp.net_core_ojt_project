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
        List<UserViewModel> GetAll(string nameSearchString, string emailSearchString, string fromSearchString, string toSearchString);
        UserViewModel Get(string title);
    }
}
