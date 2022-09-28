using DotNetCoreProject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreProject.BLL.Services.IServices
{
    public interface IPostService
    {
        List<PostViewModel> GetAll();
        PostViewModel Get(int id);
        bool Save(PostViewModel model);
        bool Update(PostViewModel model);
        bool Delete(int id);
    }
}
