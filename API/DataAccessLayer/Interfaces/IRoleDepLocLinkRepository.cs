using DataAccessLayer.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IRoleDepLocLinkRepository
    {
        public Task<int> AddRoleDepLocLink(RoleDeptLocLinkEntity roleDeptLocLinkEntity);
        public Task<List<RoleDeptLocLinkEntity>> GetAllRoleDepLocLink();
        public Task<int> GetRoleDepLocLinkId(int? RoleId, int? DeptId, int? LocId);
        public Task<List<RoleDeptLocLinkEntity>> ApplyFilter(Filter inputFilters);
    }
}
