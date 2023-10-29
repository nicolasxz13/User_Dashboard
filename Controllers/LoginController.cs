using User_Dashboard.Data;
using User_Dashboard.Models;
using User_Dashboard.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace User_Dashboard.Controllers
{
    public class LoginController : Controller
    {
        private LoginContext _context;

        public LoginController(LoginContext loginContext)
        {
            _context = loginContext;
        }

        [HttpGet("signin")]
        public IActionResult Signin()
        {
            return View();
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login_in(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                User? userInDb = _context.Users
                    .Include(a => a.UserLevel)
                    .Select(
                        a =>
                            new User
                            {
                                Id = a.Id,
                                Email = a.Email,
                                Password = a.Password,
                                UserLevel = a.UserLevel
                            }
                    )
                    .SingleOrDefault(u => u.Email == userDTO.Email);
                if (userInDb == null)
                {
                    return View("Signin");
                }

                PasswordHasher<UserDTO> hasher = new PasswordHasher<UserDTO>();

                var result = hasher.VerifyHashedPassword(
                    userDTO,
                    userInDb.Password,
                    userDTO.Password
                );

                if (result == 0)
                {
                    return View("Signin");
                }
                else
                {
                    HttpContext.Session.SetInt32("UserId", userInDb.Id);
                    HttpContext.Session.SetInt32("UserLevel", userInDb.UserLevel.Userlevel);
                    HttpContext.Session.SetString("UserLevelName", userInDb.UserLevel.Name);
                    return RedirectToAction("Index", "UserDashboard");
                }
            }
            else
            {
                return View("Signin");
            }
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }

        [HttpPost("create")]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();

                user.Password = Hasher.HashPassword(user, user.Password);

                if (!_context.Users.Any())
                {
                    UserLevel? userLevel = _context.UserLevels.FirstOrDefault(
                        a => a.Name == "admin"
                    );
                    if (userLevel != null)
                    {
                        user.UserLevel = userLevel;
                    }
                    else
                    {
                        return RedirectToAction("Register");
                    }
                }
                else
                {
                    UserLevel? userLevel = _context.UserLevels.FirstOrDefault(
                        a => a.Name == "normal"
                    );
                    if (userLevel != null)
                    {
                        user.UserLevel = userLevel;
                    }
                    else
                    {
                        return RedirectToAction("Register");
                    }
                }

                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;
                _context.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Signin");
            }
            else
            {
                return View("Register");
            }
        }
    }
}
