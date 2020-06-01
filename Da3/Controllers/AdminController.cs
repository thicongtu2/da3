using System.Linq;
using System.Threading.Tasks;
using Da3.Core.Role;
using Da3.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Da3.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        public AdminController(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        [Authorize(Policy = PolicyRule.Restricted)]
        public IActionResult Account()
        {
            return View();
        }
        
        [Authorize(Policy = PolicyRule.Restricted)]
        public async Task<object> All()
        {
            var accounts = _dbContext.Employers.ToList();
            return new { data = accounts };
        } 
    }
}