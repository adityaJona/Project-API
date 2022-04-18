using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public IConfiguration configuration;
        public EmployeesController(EmployeeRepository employeeRepository, IConfiguration configuration) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this.configuration = configuration;
        }
        //[Authorize(Roles = "Employee")]
        [HttpGet("MasterData")]
        public ActionResult GetMasterData()
        {
            var result = employeeRepository.GetMasterData();
            return Ok(new { status = HttpStatusCode.OK, Result = result, Message = "DATA DITEMUKAN" });
            //return Ok(result);
        }

        [HttpGet("TestCors")]
        public ActionResult TestCors()
        {
            //return Ok("Test Cors Berhasil");
            var result = employeeRepository.GetMasterData();
            return Ok(result);
        }

        
    }
}
