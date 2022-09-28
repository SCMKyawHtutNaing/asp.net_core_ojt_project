using DotNetCoreProject.DTO;
using DotNetCoreProject.Entity.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreProject.DAL.IRepositories
{
    public interface IEmployeeRepository
    {
        List<EmployeeViewModel> GetAll();
        EmployeeViewModel Get(int id);
        bool Save(Employee obj);
        bool Update(Employee obj);
        bool Delete(int id);
    }
}
