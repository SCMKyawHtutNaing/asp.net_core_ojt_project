using AutoMapper;
using DotNetCoreProject.BLL.Services.IServices;
using DotNetCoreProject.DAL.IRepositories;
using DotNetCoreProject.DTO;
using DotNetCoreProject.Entity.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreProject.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public List<EmployeeViewModel> GetAll()
        {
            List<EmployeeViewModel> lst = _employeeRepository.GetAll();
            return lst;
        }

        public EmployeeViewModel Get(int id)
        {
            EmployeeViewModel model = _employeeRepository.Get(id);
            return model;
        }

        public bool Save(EmployeeViewModel model)
        {
            bool success = _employeeRepository.Save(_mapper.Map<Employee>(model));
            return success;
        }

        public bool Update(EmployeeViewModel model)
        {
            bool success = _employeeRepository.Update(_mapper.Map<Employee>(model));
            return success;
        }

        public bool Delete(int id)
        {
            bool success = _employeeRepository.Delete(id);
            return success;
        }
    }
}
