using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class DepartmentRepository: IDepartmentRepository
    {
        private readonly EmployeeDirectoryContext _employeeDirectoryContext;
        public DepartmentRepository(EmployeeDirectoryContext employeeDirectoryContext)
        {
            _employeeDirectoryContext = employeeDirectoryContext;
        }
        public async Task<List<DepartmentEntity>> GetDepartments()
        {
            return _employeeDirectoryContext.Departments.ToList();
        
        }
        public async Task<bool> AddDepartmentToDb(DepartmentEntity department)
        {
            _employeeDirectoryContext.Departments.Add(department);
            _employeeDirectoryContext.SaveChanges();
            return true;
        }
    }
}
