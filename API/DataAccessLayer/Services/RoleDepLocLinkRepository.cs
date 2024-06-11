using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class RoleDepLocLinkRepository:IRoleDepLocLinkRepository
    {
        private readonly EmployeeDirectoryContext _employeeDirectoryContext;
        public RoleDepLocLinkRepository(EmployeeDirectoryContext employeeDirectoryContext) 
        {
            _employeeDirectoryContext = employeeDirectoryContext;
        }
        public async Task<int> AddRoleDepLocLink(RoleDeptLocLinkEntity roleDeptLocLinkEntity)
        {
            int result= _employeeDirectoryContext.RoleDeptLocLinks.Where(r=>r.RoleId==roleDeptLocLinkEntity.RoleId && r.LocationId==roleDeptLocLinkEntity.LocationId && r.DepartmentId==roleDeptLocLinkEntity.DepartmentId).Count();
            if (result>0)
            {
                return result;
            }
            roleDeptLocLinkEntity.Id = 0;
            Console.WriteLine(roleDeptLocLinkEntity.Id);
            _employeeDirectoryContext.RoleDeptLocLinks.Add(roleDeptLocLinkEntity);
            _employeeDirectoryContext.SaveChanges();
            return _employeeDirectoryContext.RoleDeptLocLinks.Select(r => r.Id).Max();
        }
        public async Task<List<RoleDeptLocLinkEntity>> GetAllRoleDepLocLink()
        {
            return _employeeDirectoryContext.RoleDeptLocLinks.Include(r => r.Role).Include(r => r.Location).Include(r => r.Department).ToList();
        }
        public async Task<int> GetRoleDepLocLinkId(int? RoleId,int? DeptId,int? LocId)
        {
            return _employeeDirectoryContext.RoleDeptLocLinks.Where(r=> (r.RoleId==RoleId && r.DepartmentId==DeptId && r.LocationId == LocId)).Select(r=>r.Id).FirstOrDefault();
        }
        public async Task<List<RoleDeptLocLinkEntity>> ApplyFilter(Filter inputFilters)
        {
            return _employeeDirectoryContext.RoleDeptLocLinks.Where(role=>(inputFilters.Locations.Contains(role.LocationId)||inputFilters.Locations.Length==0)&& (inputFilters.Departments.Contains(role.DepartmentId)|| inputFilters.Departments.Length==0)).Include(r=>r.Department).Include(r=>r.Location).Include(r=>r.Role).ToList();
        }
    }
}
