using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class RoleRepository:IRoleRepository
    {
        private readonly EmployeeDirectoryContext _employeeDirectoryContext;

        public RoleRepository(EmployeeDirectoryContext employeeDirectoryContext)
        {
            _employeeDirectoryContext = employeeDirectoryContext;
        }
        public async Task<int> AddRoleToDb(RoleEntity role)
        {
           int result= _employeeDirectoryContext.Roles.Where(r=>r.Name.ToUpper()==role.Name.ToUpper()).Select(r=>r.Id).FirstOrDefault();
            if (result > 0)
                return result;
            _employeeDirectoryContext.Roles.Add(role);
            _employeeDirectoryContext.SaveChanges();
            return _employeeDirectoryContext.Roles.Select(r=>r.Id).Max();
        }

/*        public async Task<List<RoleEntity>> GetRoles()
        {
            List<RoleEntity> roles = _employeeDirectoryContext.Roles.Include(r => r.Department).Include(l => l.Location).ToList();
            for (int i = 0; i < roles.Count; i++)
                Console.WriteLine(roles[i].Department.Name + " " + roles[i].Department.Name);
            return (from role in Roles select role).ToList();
        }*/
    }
}
