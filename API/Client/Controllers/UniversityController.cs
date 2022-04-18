using API.Models;
using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class UniversityController : BaseController<University, UniversityRepository, int>
    {
        private readonly UniversityRepository _universityRepository;
        public UniversityController(UniversityRepository repository) : base(repository)
        {
            this._universityRepository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
