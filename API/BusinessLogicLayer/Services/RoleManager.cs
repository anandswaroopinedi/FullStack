using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Models;

namespace BusinessLogicLayer.Managers
{
    public class RoleManager : IRoleManager
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleDepLocLinkRepository _roleDepLocLinkRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private static string filePath = @"C:\Users\anand.i\source\repos\Employee Directory Console App\Data\Repository\Role.json";
        public RoleManager(IRoleRepository roleRepository,IRoleDepLocLinkRepository roleDepLocLinkRepository,IMapper mapper,IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            _roleDepLocLinkRepository = roleDepLocLinkRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<string> AddRole(Role role, string[] employees)
        {
            role.Id = await _roleRepository.AddRoleToDb(_mapper.Map<RoleEntity>(role));
            if (role.DepartmentId != null && role.LocationId != null)
            {
                try
                {
                    int id = await _roleDepLocLinkRepository.AddRoleDepLocLink(_mapper.Map<RoleDeptLocLinkEntity>(role));
                    if (await _employeeRepository.updateRoletoEmployee(employees, id))
                    {
                        return "Role Added Successfully and employees are updated with role";
                    }
                    return "Role Added Successfully";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return "Role Addition Unsuccessful";
                }
            }
            return "Role Addition Successful without Department and Location";
        }
        public async Task<bool> CheckRoleExists(string roleName)
        {
            List<Role> roleList = await GetAll();
            for (int i = 0; i < roleList.Count; i++)
            {
                if (roleList[i].Name == roleName)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<List<Role>> GetAll()
        {
            return _mapper.Map<List<Role>>( await _roleDepLocLinkRepository.GetAllRoleDepLocLink());
        }
        public async Task<string> GetRoleName(int id)
        {
            List<Role> rolesList = await GetAll();
            for (int i = 0; i < rolesList.Count; i++)
            {
                if (rolesList[i].Id == id)
                {
                    return rolesList[i].Name;
                }
            }
            return "None";
        }
        public async Task<List<Role>> ApplyFilter(Filter inputFilters)
        {
            return _mapper.Map<List<Role>>(await _roleDepLocLinkRepository.ApplyFilter(inputFilters));
        }
    }
}
