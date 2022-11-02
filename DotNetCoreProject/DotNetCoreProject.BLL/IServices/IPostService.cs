using DotNetCoreProject.DTO;
using System.Data;

namespace DotNetCoreProject.BLL.Services.IServices
{
    public interface IPostService
    {
        List<PostViewModel> GetAll(string searchString);
        PostViewModel Get(int id);
        PostViewModel Get(string title);
        bool Save(PostViewModel model);
        bool Update(PostViewModel model);
        bool Delete(int id);
        DataTable GetDataTableForDownload(string searchString);
    }
}
