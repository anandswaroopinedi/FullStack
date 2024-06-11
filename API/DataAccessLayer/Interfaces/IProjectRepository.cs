using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IProjectRepository
    {
        Task<bool> AddProjectToDb(ProjectEntity project);
        Task<List<ProjectEntity>> GetProjects();
    }
}
