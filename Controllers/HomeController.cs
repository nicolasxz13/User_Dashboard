
using Microsoft.AspNetCore.Mvc;
using User_Dashboard.Data;

namespace User_Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private LoginContext _context;

        public HomeController(LoginContext loginContext)
        {
            _context = loginContext;
        }

        [HttpGet("")]
        public IActionResult Index(){
            return View();
        }
    }
    }