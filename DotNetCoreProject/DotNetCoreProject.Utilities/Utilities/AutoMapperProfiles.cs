using AutoMapper;
using DotNetCoreProject.DTO;
using DotNetCoreProject.Entity.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreProject.Utilities.Utilities
{
    public class AutoMapperProfiles
    {
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            }
        }
    }
}
