using AutoMapper;
using DataAccessLayer.Interfaces;
using Models;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.Managers
{
    public class StatusManager : IStatusManager
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public StatusManager(IStatusRepository statusRepository, IMapper mapper)
        {
            _statusRepository = statusRepository;
            _mapper = mapper;
        }
        public async Task<List<Status>> GetStatuses()
        {
            return _mapper.Map<List<Status>>(await _statusRepository.GetStatuses());
        }
    }
}
