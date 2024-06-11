using DataAccessLayer.Entities;
namespace DataAccessLayer.Interfaces
{
    public interface IRoleRepository
    {
        public Task<int> AddRoleToDb(RoleEntity role);
    }
}
