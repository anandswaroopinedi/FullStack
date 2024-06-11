using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleManager _roleManager;
        public RoleController(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }
        // GET: api/<RoleController>
        [HttpGet("all")]
        public async Task<IEnumerable<Role>> Get()
        {
            return await _roleManager.GetAll();
        }
        [HttpPost("create")]
        public async Task<string> Post([FromBody] Role role,[FromQuery]string[] employees)
        {
            if (employees[0] == null)
                employees = [];
            return JsonSerializer.Serialize<string>( await _roleManager.AddRole(role, employees));
        }
        [HttpPost("filter")]
        public async Task<IEnumerable<Role>> ApplyFilters(Filter inputFilters)
        {
            return await _roleManager.ApplyFilter(inputFilters);
        }

    }
}
