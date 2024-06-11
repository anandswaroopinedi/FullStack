using Models;

namespace BusinessLogicLayer.Interfaces
{
    public interface IRoleManager
    {
        public Task<string> AddRole(Role rolesModel, string[] employees);
        public Task<bool> CheckRoleExists(string roleName);
        public Task<List<Role>> GetAll();
        public Task<string> GetRoleName(int id);
        public Task<List<Role>> ApplyFilter(Filter inputFilters);
    }
}
