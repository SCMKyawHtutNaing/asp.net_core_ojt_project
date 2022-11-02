using DotNetCoreProject.DAL.IRepositories;
using DotNetCoreProject.DTO;
using DotNetCoreProject.Entity.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreProject.DAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DBContext _context;
        public EmployeeRepository(DBContext context)
        {
            _context = context;
        }

        public List<EmployeeViewModel> GetAll()
        {
            var query = (from data in _context.Employees
                         select new EmployeeViewModel
                         {
                             EmployeeId = data.EmployeeId,
                             Name = data.Name,
                             Address = data.Address,
                             Designation = data.Designation,
                             Salary = data.Salary,
                             JoiningDate = data.JoiningDate
                         });
       
            return query.ToList();
        }

        public EmployeeViewModel Get(int id)
        {
            var query = (from data in _context.Employees
                         where data.EmployeeId == id
                         select new EmployeeViewModel
                         {
                             EmployeeId = data.EmployeeId,
                             Name = data.Name,
                             Address = data.Address,
                             Designation = data.Designation,
                             Salary = data.Salary,
                             JoiningDate = data.JoiningDate
                         });

            return query.FirstOrDefault();
        }

        public bool Save(Employee obj)
        {
            bool success = false;
            try
            {
                _context.Add(obj);
                _context.SaveChanges();

                success = true;

            }
            catch (Exception ex)
            {

            }

            return success;
        }

        public bool Update(Employee obj)
        {
            bool success = false;
            try
            {
                _context.Employees.Update(obj);
                _context.SaveChanges();

                success = true;

            }
            catch (Exception ex)
            {

            }

            return success;
        }

        public bool Delete(int id)
        {
            bool success = false;
            try
            {
                Employee employee = _context.Employees.FirstOrDefault(x => x.EmployeeId == id);

                _context.Employees.Remove(employee);
                _context.SaveChanges();

                success = true;

            }
            catch (Exception ex)
            {

            }

            return success;
        }

    }
}
