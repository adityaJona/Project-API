using API.Models;
using API.ViewModels;
using Client.Base;
using Client.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class LoginRepository : GeneralRepository<Account, string>
    {
        //private readonly Address address;
        //private readonly HttpClient httpClient;
        //private readonly string request;
        //private readonly IHttpContextAccessor _contextAccessor;

        public LoginRepository(Address address, string request = "Account/") : base(address, request)
        {
            //this.address = address;
            //this.request = request;
            //_contextAccessor = new HttpContextAccessor();
            //httpClient = new HttpClient
            //{
            //    BaseAddress = new Uri(address.link)
            //};
        }

        public LoginResponseVM Login(LoginVM loginVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");
            LoginResponseVM results;
            using (var response = httpClient.PostAsync(request + "Login", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                results = JsonConvert.DeserializeObject<LoginResponseVM>(apiResponse);
            }
            return results;
        }
    }
}
