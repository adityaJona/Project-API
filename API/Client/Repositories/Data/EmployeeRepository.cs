using API.Models;
using API.ViewModels;
using Client.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        //private readonly Address address;
        //private readonly HttpClient httpClient;
        //private readonly string request;
        //private readonly IHttpContextAccessor _contextAccessor;
        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            //this.address = address;
            //this.request = request;
            //_contextAccessor = new HttpContextAccessor();
            //httpClient = new HttpClient
            //{
            //    BaseAddress = new Uri(address.link)
            //};
        }

        public async Task<IEnumerable<Employee>> GetMasterData()
        {
            List<Employee> entities = new List<Employee>();
            using (var response = await httpClient.GetAsync(request + "MasterData"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<Employee>>(apiResponse);
            }
            return entities;
        }
    }
}
