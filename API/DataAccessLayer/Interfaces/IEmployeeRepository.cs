using DataAccessLayer.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<int> GetEmployeesCount();
        public Task<bool> AddEmployeeToDb(EmployeeEntity employee);
        public Task<List<EmployeeEntity>> GetEmployees(int pageNumber, int recordsPerPage);
        public Task<EmployeeEntity> GetEmployeeById(string Id);

        public Task<bool> UpdateEmployee(EmployeeEntity employee);

        public Task<bool> DeleteEmployees(string[] ids);
        public Task<List<string>> GetAllEmployeeIds();

        public Task<List<EmployeeEntity>> ApplyFilters(Filter filterEmployee, int pageNumber, int recordsPerPage);

        public Task<IEnumerable<EmployeeEntity>> ApplySorting(string property, string order, int pageNumber, int recordsPerPage);
        public Task<List<EmployeeEntity>> GetEmployeesWithNotAssignedRole(string name);
        public Task<bool> updateRoletoEmployee(string[] ids, int roledeptloclinkId);
        public Task<List<EmployeeEntity>> GetEmployeesByRoleId(int id);

    }
}
