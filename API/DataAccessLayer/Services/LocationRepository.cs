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
    public class LocationRepository:ILocationRepository
    {
        private readonly EmployeeDirectoryContext _employeeDirectoryContext;

        public LocationRepository(EmployeeDirectoryContext employeeDirectoryContext)
        {
            _employeeDirectoryContext = employeeDirectoryContext;
        }
        public async Task<List<LocationEntity>> GetLocations()
        {
            return _employeeDirectoryContext.Locations.ToList();
        }
        public async Task<bool> AddLocationToDb(LocationEntity location)
        {
            _employeeDirectoryContext.Locations.Add(location);
            _employeeDirectoryContext.SaveChanges();
            return true;
        }
    }
}
