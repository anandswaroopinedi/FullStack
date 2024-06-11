using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Services
{
    public class StatusRepository : IStatusRepository
    {
        private readonly EmployeeDirectoryContext _employeeDirectoryContext;
        public StatusRepository(EmployeeDirectoryContext employeeDirectoryContext)
        {
            _employeeDirectoryContext = employeeDirectoryContext;
        }
        public async Task<IEnumerable<StatusEntity>> GetStatuses()
        {
            return _employeeDirectoryContext.Statuses.ToList();
        }
    }
}
