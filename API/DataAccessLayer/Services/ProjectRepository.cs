using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{


    public class ProjectRepository : IProjectRepository
    {
        private readonly EmployeeDirectoryContext _employeeDirectoryContext;
        public ProjectRepository(EmployeeDirectoryContext employeeDirectoryContext)
        {
            _employeeDirectoryContext = employeeDirectoryContext;
        }
        public async Task<List<ProjectEntity>> GetProjects()
        {
            return _employeeDirectoryContext.Projects.ToList();
        }
        public async Task<bool> AddProjectToDb(ProjectEntity project)
        {
            _employeeDirectoryContext.Projects.Add(project);
            _employeeDirectoryContext.SaveChanges();
            return true;
        }
    }
}
