using DataAccessLayer.Interfaces;
using BusinessLogicLayer.Interfaces;
using Models;
using AutoMapper;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Managers
{
    public class LocationManager : ILocationManager
    {
        private readonly ILocationRepository _locationRepository;
        private static string filePath = @"C:\Users\anand.i\source\repos\Employee Directory Console App\Data\Repository\Location.json";
        private readonly IMapper _mapper;
        public LocationManager(ILocationRepository locationRepository,IMapper mapper)
        {
            _locationRepository = locationRepository;  
            _mapper = mapper;
        }

        public async Task<bool> AddLocation(Location location)
        {
            List<Location> locationList =GetAll().Result;

            if (!CheckLocationExists(location.Name, locationList))
            {
                locationList.Add(location);
                if(await _locationRepository.AddLocationToDb(_mapper.Map<LocationEntity>(location)))
                {
                    return true;
                }
            }
                return false;
        }

        public async Task<List<Location>> GetAll()
        {
            return _mapper.Map<List<Location>>( await _locationRepository.GetLocations());
        }
        public static bool CheckLocationExists(string loc, List<Location> locationList)
        {
            for (int i = 0; i < locationList.Count; i++)
            {
                if (locationList[i].Name == loc)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
