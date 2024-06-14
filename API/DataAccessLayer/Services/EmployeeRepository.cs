using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace DataAccessLayer.Services
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly EmployeeDirectoryContext _employeeDirectoryContext;

        public EmployeeRepository(EmployeeDirectoryContext employeeDirectoryContext)
        {
            _employeeDirectoryContext = employeeDirectoryContext;
        }

        public async Task<bool> AddEmployeeToDb(EmployeeEntity employee)
        {
            //employee.ProfileImage = "assets/" + employee.ProfileImage.Split('\\').Last();
            Console.WriteLine(employee.ProfileImage);
/*            employee.StatusId = 1;*/
            _employeeDirectoryContext.Employees.Add(employee);
            _employeeDirectoryContext.SaveChanges();
            return true;
        }
        public async Task<List<EmployeeEntity>> GetEmployees(int pageNumber,int recordsPerPage)
        {
            return _employeeDirectoryContext.Employees.Skip((pageNumber - 1) * recordsPerPage)
        .Take(recordsPerPage).Include(r => r.Project).Include(r => r.RoleDeptLoc).Include(r => r.RoleDeptLoc.Location).Include(r => r.RoleDeptLoc.Department).Include(r => r.RoleDeptLoc.Role).Include(r => r.Status).ToList();
        }
        public async Task<List<EmployeeEntity>> GetEmployeesWithNotAssignedRole(string name)
        {
            int activeId = _employeeDirectoryContext.Statuses.Where(x => x.Name.ToLower() == "active").Select(x => x.Id).FirstOrDefault();
            if (name == "$")
            {
                return _employeeDirectoryContext.Employees.Where(x => x.RoleDeptLocId == null && x.StatusId==activeId).ToList();
            }
            else
            {
                return _employeeDirectoryContext.Employees.Where(x => x.RoleDeptLocId == null && x.FirstName.ToUpper().StartsWith(name.ToUpper()) && x.StatusId == activeId).ToList();
            }
        }
        public async Task<EmployeeEntity> GetEmployeeById(string Id)
        {
            return _employeeDirectoryContext.Employees.Where(emp=>emp.Id==Id).Include(r=>r.RoleDeptLoc.Location).Include(r => r.RoleDeptLoc.Role).Include(r => r.RoleDeptLoc.Department).ToList()[0];
        }
        public async Task<List<EmployeeEntity>> GetEmployeesByRoleId(int id)
        {
            return _employeeDirectoryContext.Employees.Where(emp=>emp.RoleDeptLocId==id).Include(r => r.RoleDeptLoc.Location).Include(r => r.RoleDeptLoc.Role).Include(r => r.RoleDeptLoc.Department).ToList();
        }
        public async Task<bool> UpdateEmployee(EmployeeEntity employee)
        {
            try
            {
                _employeeDirectoryContext.Employees.Update(employee);
                _employeeDirectoryContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
                // Provide for exceptions.
            }

        }
        public async Task<bool> updateRoletoEmployee(string []ids,int roledeptloclinkId)
        {
            try
            {
                if(ids.Length==0)
                    return false;
                for (int i = 0; i < ids.Length; i++)
                {
                    EmployeeEntity employee = _employeeDirectoryContext.Employees.Where(x => x.Id == ids[i]).FirstOrDefault();
                    if (employee != null)
                    {
                        employee.RoleDeptLocId = roledeptloclinkId;
                    }
                    _employeeDirectoryContext.Employees.Update(employee);
                }
                _employeeDirectoryContext.SaveChanges();
                return true;
            }
            catch { return false; }
        }
        public async Task<bool> DeleteEmployees(string[] ids)
        {

            for (int i = 0; i < ids.Length; i++)
            {
                Console.WriteLine(ids[0]);
                EmployeeEntity emp=_employeeDirectoryContext.Employees.Where(x => x.Id == ids[i]).FirstOrDefault(); 
                emp.StatusId=_employeeDirectoryContext.Statuses.Where(x=>x.Name.ToLower()=="in active").Select(x=>x.Id).FirstOrDefault();
                _employeeDirectoryContext.Employees.Update(emp);
            }
            _employeeDirectoryContext.SaveChanges();
            return true;
        }
        public async Task<List<string>> GetAllEmployeeIds()
        {
            return _employeeDirectoryContext.Employees.Select(emp => emp.Id).ToList();
        }

        public async Task<List<EmployeeEntity>> ApplyFilters(Filter filterEmployee, int pageNumber, int recordsPerPage)
        {
            Console.WriteLine(filterEmployee.Alphabet);
            Console.WriteLine(filterEmployee.Departments.Length);
            Console.WriteLine(filterEmployee.Locations.Length);
            Console.WriteLine(filterEmployee.Statuses);

            return _employeeDirectoryContext.Employees.Where(employee => 
          (filterEmployee.Alphabet == "$" || employee.FirstName.StartsWith(filterEmployee.Alphabet.ToUpper())) &&
          (filterEmployee.Locations.Length == 0 || filterEmployee.Locations.Contains(employee.RoleDeptLoc.LocationId)) &&
          (filterEmployee.Departments.Length == 0 || filterEmployee.Departments.Contains(employee.RoleDeptLoc.DepartmentId))
          && (filterEmployee.Statuses.Length == 0 || filterEmployee.Statuses.Contains(employee.Status.Id))).Include(r => r.Project).Include(r => r.RoleDeptLoc).Include(r => r.RoleDeptLoc.Location).Include(r => r.RoleDeptLoc.Department).Include(r => r.RoleDeptLoc.Role).Include(r => r.Status).Skip((pageNumber - 1) * recordsPerPage)
        .Take(recordsPerPage).ToList();
        }
        public async Task<IEnumerable<EmployeeEntity>> ApplySorting(string property, string order, int pageNumber, int recordsPerPage)
        {
            if (!string.IsNullOrEmpty(property))
            {
                var propertyInfo = typeof(EmployeeEntity).GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(EmployeeEntity), "e");
                    var propertyAccessExpression = Expression.Property(parameter, propertyInfo);
                    var sortExpression = Expression.Lambda<Func<EmployeeEntity, object>>(propertyAccessExpression, parameter);

                    if (order.Equals("asc", StringComparison.OrdinalIgnoreCase))
                    {
                        return _employeeDirectoryContext.Employees.Include(r => r.Project).Include(r => r.RoleDeptLoc).Include(r => r.RoleDeptLoc.Location).Include(r => r.RoleDeptLoc.Department).Include(r => r.RoleDeptLoc.Role).Include(r => r.Status).OrderBy(sortExpression).Skip((pageNumber - 1) * recordsPerPage)
        .Take(recordsPerPage).ToList();
                    }
                    else if (order.Equals("desc", StringComparison.OrdinalIgnoreCase))
                    {
                        return _employeeDirectoryContext.Employees.Include(r => r.Project).Include(r => r.RoleDeptLoc).Include(r => r.RoleDeptLoc.Location).Include(r => r.RoleDeptLoc.Department).Include(r => r.RoleDeptLoc.Role).Include(r => r.Status).OrderByDescending(sortExpression).Skip((pageNumber - 1) * recordsPerPage)
        .Take(recordsPerPage).ToList();
                    }
                }
            }
            return [];
        }
        public async Task<int> GetEmployeesCount()
        {
            return _employeeDirectoryContext.Employees.Count();
        }
        private async Task<List<EmployeeEntity>> JoinTable(DbSet<EmployeeEntity> employees)
        {
            return  employees.Include(r => r.Project).Include(r => r.RoleDeptLoc).Include(r => r.RoleDeptLoc.Location).Include(r => r.RoleDeptLoc.Department).Include(r => r.RoleDeptLoc.Role).Include(r => r.Status).ToList();
        }
    }
}
