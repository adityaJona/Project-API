using API.Models;
using API.ViewModels;
using Client.Base;
using Client.Models;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository _accountRepository;
        public AccountController(AccountRepository repository) : base(repository)
        {
            this._accountRepository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        

    }
}
