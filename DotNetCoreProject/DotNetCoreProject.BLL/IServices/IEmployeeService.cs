using DotNetCoreProject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreProject.BLL.Services.IServices
{
    public interface IEmployeeService
    {
        List<EmployeeViewModel> GetAll();
        EmployeeViewModel Get(int id);
        bool Save(EmployeeViewModel model);
        bool Update(EmployeeViewModel model);
        bool Delete(int id);
    }
}
