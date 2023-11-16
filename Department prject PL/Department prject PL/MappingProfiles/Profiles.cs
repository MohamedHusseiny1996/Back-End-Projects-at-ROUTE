using AutoMapper;
using Department_prject_PL.Models;
using Department_project_DAL.Entities;

namespace Department_prject_PL.MappingProfiles
{
    public class Profiles:Profile
    {
        public Profiles()
        {
            CreateMap<Employee,EmployeeViewModel>().ReverseMap();
            CreateMap<Department, DepartmentViewModel>().ReverseMap();
        }
    }
}
