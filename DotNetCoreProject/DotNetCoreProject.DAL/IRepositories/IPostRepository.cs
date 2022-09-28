using DotNetCoreProject.DTO;
using DotNetCoreProject.Entity.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreProject.DAL.IRepositories
{
    public interface IPostRepository
    {
        List<PostViewModel> GetAll();
        PostViewModel Get(int id);
        bool Save(Post obj);
        bool Update(Post obj);
        bool Delete(int id);
    }
}
