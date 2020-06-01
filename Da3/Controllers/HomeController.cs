using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Da3.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Da3.Models;
using Microsoft.AspNetCore.Authorization;

namespace Da3.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<object> Job(double latitude, double longitude)
        {
            var jobs = _dbContext.Jobs.Where(i => i.CreatedTime.AddDays(3) >= DateTime.Today).ToList();
            return new { data = jobs };
        }
        
        [HttpGet]
        public IActionResult View(int id)
        {
            var job = _dbContext.Jobs.FirstOrDefault(i => i.Id == id);
            return View(job);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}