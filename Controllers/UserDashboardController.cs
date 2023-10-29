using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User_Dashboard.Data;
using User_Dashboard.Filters;
using User_Dashboard.Models;

namespace User_Dashboard.Controllers
{
    [SessionCheck]
    public class UserDashboardController : Controller
    {
        private LoginContext _context;

        public UserDashboardController(LoginContext loginContext)
        {
            _context = loginContext;
        }

        [HttpGet("dashboard")]
        public IActionResult Index()
        {
            string? userlevelname = HttpContext.Session.GetString("UserLevelName");
            Console.WriteLine(userlevelname);
            if (userlevelname != null && userlevelname == "admin")
            {
                return RedirectToAction("Dashboardadmin", "UserDashboard");
            }
            else
            {
                List<User> users = _context.Users.Include(a => a.UserLevel).ToList();
                return View(users);
            }
        }

        [SessionAdministrator]
        [HttpGet("dashboard/admin")]
        public IActionResult Dashboardadmin()
        {
            List<User> users = _context.Users.Include(a => a.UserLevel).ToList();
            return View(users);
        }
    }
}
