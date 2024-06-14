using Models;

namespace BusinessLogicLayer.Interfaces
{
    public interface IEmployeeManager
    {
        public Task<bool> Create(Employee employee);
        public Task<bool> Update(Employee employee);
        public Task<bool> DeleteEmployees(string[] ids);
        public Task<List<Employee>> GetAllEmployees(int pageNumber, int recordsPerPage);
        public Task<List<Employee>> GetEmployeesWithNotAssignedRole(string name);
        public Task<List<string>> GetAllEmployeeIds();
        public Task<List<Employee>> ApplyFilter(Filter filterEmployee, int pageNumber, int recordsPerPage);
        public Task<IEnumerable<Employee>> ApplySorting(string property, string order, int pageNumber, int recordsPerPage);
        public Task<Employee> GetEmployeeById(string id);
        public Task<List<Employee>> GetEmployeesByRoleId(int id);
        public Task<int> GetEmployeesCount();
    }
}
