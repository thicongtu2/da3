using System;
using System.Linq;
using System.Threading.Tasks;
using Da3.Core.Entities;
using Da3.Core.Role;
using Da3.Infrastructure.Database;
using Da3.Share.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Da3.Controllers
{
    [Authorize(Policy = PolicyRule.Employer)]
    public class EmployerController : BaseController
    {
        public EmployerController(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        [HttpGet]
        public IActionResult Info()
        {
            var info = _dbContext.Employers.FirstOrDefault(i => i.UserId == User.GetUserId());
            return View(info);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Info(Employer employer)
        {
            if (ModelState.IsValid)
            {
                var info = _dbContext.Employers.FirstOrDefault(i => i.UserId == User.GetUserId());
                if (info!=null)
                {
                    info.Address = employer.Address;
                    info.Description = employer.Description;
                    info.Name = employer.Name;
                    info.Phone = employer.Phone;
                    _dbContext.Update(info);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    employer.UserId = User.GetUserId();
                    _dbContext.Add(employer);
                    await _dbContext.SaveChangesAsync();
                }
            }
            
            return View(employer);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Job job)
        {
            if (ModelState.IsValid)
            {
                job.EmployerId = _dbContext.Employers.FirstOrDefault(i => i.UserId == User.GetUserId()).Id;
                _dbContext.Add(job);
                await _dbContext.SaveChangesAsync();
            }

            return View();
        }

        public IActionResult List()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<object> All()
        {
            var employerId = _dbContext.Employers.FirstOrDefault(i => i.UserId == User.GetUserId()).Id;
            var jobs = _dbContext.Jobs.Where(i => i.EmployerId == employerId).ToList();
            return new { data = jobs };
        }
    }
}