using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IDepartmentRepository
    {
        public Task<List<DepartmentEntity>> GetDepartments();
        public Task<bool> AddDepartmentToDb(DepartmentEntity department);
    }
}
