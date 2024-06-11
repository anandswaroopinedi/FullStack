using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace AspWebApi.Controllers
{
    public class StatusController : Controller
    {
        private readonly IStatusManager _statusManager;
        public StatusController(IStatusManager statusManager)
        {
            _statusManager = statusManager;
        }
        [HttpGet("all")]
        public async Task<List<Status>> GetAllStatuses()
        {

            return await _statusManager.GetStatuses();
        }
    }
}
