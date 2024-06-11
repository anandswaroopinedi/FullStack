using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ILocationRepository
    {
        public Task<List<LocationEntity>> GetLocations();
        public Task<bool> AddLocationToDb(LocationEntity location);
    }
}
