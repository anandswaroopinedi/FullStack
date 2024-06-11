﻿using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeManager _employeeManager;
        public EmployeeController(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }
        // GET: api/<ValuesController>
        [HttpGet("all")]
        [Authorize]
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            List<Employee> e = await _employeeManager.GetAllEmployees();
            return await _employeeManager.GetAllEmployees();
        }
        [HttpGet("with-role-null")]
        public async Task<List<Employee>> GetEmployeesWithNotAssignedRole(string name)
        {
            return await _employeeManager.GetEmployeesWithNotAssignedRole(name);

        }
        [HttpGet("ids")]
        public async Task<IEnumerable<string>> GetAllEmployeeIds()
        {

            return await _employeeManager.GetAllEmployeeIds();
        }

        // POST api/<ValuesController>
        [HttpPost("create")]
        public async Task<bool> AddEmployee(Employee employee)
        {
            return await _employeeManager.Create(employee);
        }
        [HttpPost("update")]
        public async Task<bool> UpdateEmployee(Employee employee)
        {
            return await _employeeManager.Update(employee);
        }


        [HttpDelete("multi-delete")]
        public async Task<IEnumerable<Employee>> DeleteEmployees([FromBody] string[] ids)
        {
            Console.WriteLine(ids);
            if (ids.Length > 0)
            {
                await _employeeManager.DeleteEmployees(ids);
            }
            return await _employeeManager.GetAllEmployees();
        }
        [HttpPost("filter")]
        public async Task<IEnumerable<Employee>> ApplyFiltersOnEmployee(Filter inputFilters)
        {
            return await _employeeManager.ApplyFilter(inputFilters);
        }
        [HttpGet("sort")]
        public async Task<IEnumerable<Employee>> ApplySorting(string property, string order)
        {
            return await _employeeManager.ApplySorting(property, order);
        }
        [HttpGet("fetch-through-id")]
        public async Task<Employee> GetEmployeeById(string id)
        {
            return await _employeeManager.GetEmployeeById(id);               
        }
        [HttpGet("fetch-through-roleid")]
        public async Task<List<Employee>> GetEmployeeByRoleId(int id)
        {
            return await _employeeManager.GetEmployeesByRoleId(id);
        }

    }
}
