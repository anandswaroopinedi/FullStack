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
        public async Task<List<EmployeeEntity>> GetEmployees()
        {
            return await this.JoinTable(_employeeDirectoryContext.Employees);
        }
        public async Task<List<EmployeeEntity>> GetEmployeesWithNotAssignedRole(string name)
        {
            if (name == "$")
            {
                return _employeeDirectoryContext.Employees.Where(x => x.RoleDeptLocId == null).ToList();
            }
            else
            {
                return _employeeDirectoryContext.Employees.Where(x => x.RoleDeptLocId == null && x.FirstName.ToUpper().StartsWith(name.ToUpper())).ToList();
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
                _employeeDirectoryContext.Employees.Remove((from emp in _employeeDirectoryContext.Employees where emp.Id == ids[i].ToUpper() select emp).FirstOrDefault());
            }
            _employeeDirectoryContext.SaveChanges();
            return true;
        }
        public async Task<List<string>> GetAllEmployeeIds()
        {
            return _employeeDirectoryContext.Employees.Select(emp => emp.Id).ToList();
        }

        public async Task<List<EmployeeEntity>> ApplyFilters(Filter filterEmployee)
        {
            Console.WriteLine(filterEmployee.Alphabet);
            Console.WriteLine(filterEmployee.Departments.Length);
            Console.WriteLine(filterEmployee.Locations.Length);
            Console.WriteLine(filterEmployee.Statuses);

            return _employeeDirectoryContext.Employees.Where(employee =>
          (filterEmployee.Alphabet == "$" || employee.FirstName.StartsWith(filterEmployee.Alphabet.ToUpper())) &&
          (filterEmployee.Locations.Length == 0 || filterEmployee.Locations.Contains(employee.RoleDeptLoc.LocationId)) &&
          (filterEmployee.Departments.Length == 0 || filterEmployee.Departments.Contains(employee.RoleDeptLoc.DepartmentId))
          && (filterEmployee.Statuses.Length == 0 || filterEmployee.Statuses.Contains(employee.Status.Id))).Include(r => r.Project).Include(r => r.RoleDeptLoc).Include(r => r.RoleDeptLoc.Location).Include(r => r.RoleDeptLoc.Department).Include(r => r.RoleDeptLoc.Role).Include(r => r.Status).ToList();
        }
        public async Task<IEnumerable<EmployeeEntity>> ApplySorting(string property, string order)
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
                        return _employeeDirectoryContext.Employees.OrderBy(sortExpression).Include(r => r.Project).Include(r => r.RoleDeptLoc).Include(r => r.RoleDeptLoc.Location).Include(r => r.RoleDeptLoc.Department).Include(r => r.RoleDeptLoc.Role).Include(r => r.Status).ToList();
                    }
                    else if (order.Equals("desc", StringComparison.OrdinalIgnoreCase))
                    {
                        return _employeeDirectoryContext.Employees.OrderByDescending(sortExpression).Include(r => r.Project).Include(r => r.RoleDeptLoc).Include(r => r.RoleDeptLoc.Location).Include(r => r.RoleDeptLoc.Department).Include(r => r.RoleDeptLoc.Role).Include(r => r.Status).ToList();
                    }
                }
            }
            return [];
        }
        private async Task<List<EmployeeEntity>> JoinTable(DbSet<EmployeeEntity> employees)
        {
            return  employees.Include(r => r.Project).Include(r => r.RoleDeptLoc).Include(r => r.RoleDeptLoc.Location).Include(r => r.RoleDeptLoc.Department).Include(r => r.RoleDeptLoc.Role).Include(r => r.Status).ToList();
        }
    }
}
