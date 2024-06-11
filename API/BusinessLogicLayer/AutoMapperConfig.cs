using AutoMapper;
using DataAccessLayer.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLogicLayer
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig() 
        {
            CreateMap<EmployeeEntity, Employee>()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.RoleDeptLoc.Department.Name))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.RoleDeptLoc.Department.Id))
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.RoleDeptLoc.Location.Name))
                .ForMember(dest => dest.LocationId, opt => opt.MapFrom(src => src.RoleDeptLoc.Location.Id))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name))
                .ForMember(dest => dest.JobTitleName, opt => opt.MapFrom(src => src.RoleDeptLoc.Role.Name))
                .ForMember(dest => dest.JobTitleId, opt => opt.MapFrom(src => src.RoleDeptLoc.Role.Id));    
            CreateMap<Employee, EmployeeEntity>()
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.RoleDeptLocId, opt => opt.MapFrom(src => src.RoleDeptLocId)); 
            CreateMap<Status, StatusEntity>().ReverseMap();
            CreateMap<Role, RoleEntity>().ReverseMap();
            CreateMap<Role, RoleDeptLocLinkEntity>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.LocationId, opt => opt.MapFrom(src => src.LocationId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            CreateMap<RoleDeptLocLinkEntity, Role>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location.Name))
                .ForMember(dest => dest.RoleDeptLocId, opt => opt.MapFrom(src => src.Id)); 
            CreateMap<Department,DepartmentEntity>().ReverseMap();
            CreateMap<Location,LocationEntity>().ReverseMap();
            CreateMap<Project,ProjectEntity>().ReverseMap();
            CreateMap<Status,StatusEntity>().ReverseMap();
        }

    }
}
