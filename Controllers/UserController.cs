using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User_Dashboard.Data;
using User_Dashboard.Filters;
using User_Dashboard.Models;

namespace User_Dashboard.Controllers
{
    public class UserController : Controller
    {
        private LoginContext _context;

        public UserController(LoginContext loginContext)
        {
            _context = loginContext;
        }

        [SessionCheck]
        [HttpGet("users/show/{id}")]
        public IActionResult Show(int id)
        {
            List<Message>? messages = ListMessages(id);
            User? user = FindUserWithLevel(id);

            if (user != null && messages != null)
            {
                CommentsMessagesViewModel commentsMessagesViewModel =
                    new CommentsMessagesViewModel() { User = user, Messages = messages };
                return View(commentsMessagesViewModel);
            }
            return RedirectToAction("Index", "UserDashboard");
        }

        [SessionCheck]
        [SessionAdministrator]
        [HttpGet("users/new")]
        public IActionResult New()
        {
            return View();
        }

        [SessionCheck]
        [SessionAdministrator]
        [HttpPost("users/create")]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();

                user.Password = Hasher.HashPassword(user, user.Password);

                UserLevel? userLevel = _context.UserLevels.FirstOrDefault(
                    a => a.Name == "normal"
                );
                if (userLevel != null)
                {
                    user.UserLevel = userLevel;
                }
                else
                {
                    return RedirectToAction("New");
                }

                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;
                _context.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index", "UserDashboard");
            }
            else
            {
                return View("New");
            }
        }

        [SessionCheck]
        [HttpGet("users/edit")]
        public IActionResult Edit()
        {
            int? userid = HttpContext.Session.GetInt32("UserId");
            if (userid != null)
            {
                User? user = FindUser((int)userid);
                if (user != null)
                {
                    return View(user);
                }
                return RedirectToAction("Index", "UserDashboard");
            }
            return RedirectToAction("Index", "UserDashboard");
        }

        [SessionCheck]
        [SessionAdministrator]
        [HttpGet("users/edit/{id}")]
        public IActionResult EditAdmin(int id)
        {
            User? user = FindUserWithLevel(id);
            List<UserLevel>? userLevel = _context.UserLevels.ToList();
            
            if (user != null &&userLevel != null)
            {
                UserInformationAdmin userInformationAdmin = new UserInformationAdmin()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Last_Name = user.Last_Name,
                    Email = user.Email,
                    IdUser_level = user.UserLevel.Id
                };
                UserAdminViewModel userAdminViewModel = new UserAdminViewModel()
                {
                    InformationAdmin = userInformationAdmin,
                    UserPassword = new UserPassword(),
                    UserLevels = userLevel
                };
                return View(userAdminViewModel);
            }
            return RedirectToAction("Index", "UserDashboard");
        }

        [SessionCheck]
        [HttpPost("Users/{id}/update/information")]
        public IActionResult UpdateInformation(int id, UserInformation userInformation)
        {
            int? userid = HttpContext.Session.GetInt32("UserId");
            if (userid != id)
            {
                return RedirectToAction("Index", "UserDashboard");
            }
            User? user = FindUser(id);
            if (user != null)
            {
                user.Name = userInformation.Name;
                user.Last_Name = userInformation.Last_Name;
                user.Email = userInformation.Email;
                if (ModelState.IsValid)
                {
                    _context.Users.Update(user);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "UserDashboard");
                }
                else
                {
                    return View("Edit", user);
                }
            }
            return RedirectToAction("Index", "UserDashboard");
        }
        [SessionCheck]
        [SessionAdministrator]
        [HttpPost("Users/{id}/update/information/admin")]
        public IActionResult UpdateInformationAdmin(int id,[Bind(Prefix = "InformationAdmin")] UserInformationAdmin userInformationAdmin)
        {



            List<UserLevel>? userLevels = _context.UserLevels.ToList();
            User? user = FindUserWithLevel(id);
            if (user != null)
            {
                user.Name = userInformationAdmin.Name;
                user.Last_Name = userInformationAdmin.Last_Name;
                user.Email = userInformationAdmin.Email;

                UserInformationAdmin userInformationAdminlocal = new UserInformationAdmin()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Last_Name = user.Last_Name,
                    Email = user.Email,
                    IdUser_level = user.UserLevel.Id
                };
                UserAdminViewModel userAdminViewModel = new UserAdminViewModel()
                {
                    InformationAdmin = userInformationAdminlocal,
                    UserPassword = new UserPassword(),
                    UserLevels = userLevels
                };


                UserLevel? userLevel = _context.UserLevels.FirstOrDefault(a => a.Id == userInformationAdmin.IdUser_level);

                if (userLevel == null)
                {
                    ModelState.AddModelError("IdUser_level", "Select valid user level");
                    return View("EditAdmin", userAdminViewModel);
                }
                user.UserLevel = userLevel;
                if (ModelState.IsValid)
                {
                    _context.Users.Update(user);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "UserDashboard");
                }
                else
                {
                    return View("EditAdmin", userAdminViewModel);
                }
            }
            return RedirectToAction("Index", "UserDashboard");
        }
        [SessionCheck]
        [HttpPost("Users/{id}/update/password")]
        public IActionResult UpdatePassword(int id, UserPassword userPassword)
        {
            User? user = FindUser(id);
            if (user != null)
            {
                PasswordHasher<UserPassword> Hasher = new PasswordHasher<UserPassword>();

                user.Password = Hasher.HashPassword(userPassword, userPassword.Password);
                if (ModelState.IsValid)
                {
                    _context.Users.Update(user);
                    _context.SaveChanges();
                }
                else
                {
                    user.ConfirmPassword = userPassword.ConfirmPassword;
                    return View("Edit", user);
                }
            }
            return RedirectToAction("Index", "UserDashboard");
        }
        [SessionCheck]
        [HttpPost("Users/{id}/update/description")]
        public IActionResult UpdateDescription(int id,UserDescription userDescription)
        {
            User? user = FindUser(id);
            if (user != null)
            {
                user.Description = userDescription.Description;
                if (ModelState.IsValid)
                {
                    _context.Users.Update(user);
                    _context.SaveChanges();
                }
                else
                {
                    return View("Edit", user);
                }
            }
            return RedirectToAction("Index", "UserDashboard");
        }
        [SessionCheck]
        [SessionAdministrator]
        [HttpPost("users/{id}/destroy")]
        public IActionResult Delete(int id)
        {
            User? user = _context.Users.FirstOrDefault(a => a.Id == id);

            if (user != null)
            {
                List<Message> messages = _context.Messages.Include(a => a.Comments).Include(a => a.UserMessage).Include(a => a.RecipientMessage).Where(a => a.UserMessage.Id == id || a.RecipientMessage.Id == id).ToList();

                foreach (Message message in messages)
                {
                    _context.Comments.RemoveRange(message.Comments);
                }
                _context.Messages.RemoveRange(messages);

                _context.Users.Remove(user);

                _context.SaveChanges();
            }
            return RedirectToAction("Index", "UserDashboard");
        }
        [SessionCheck]
        [HttpPost("users/{id}/messages/create")]
        public IActionResult CreateMessage([Bind(Prefix = "Newmessage")] Message message, int id)
        {
            int? userid = HttpContext.Session.GetInt32("UserId");
            if (ModelState.IsValid && userid != null)
            {
                User? Usertarget = FindUserWithLevel(id);
                User? user = FindUserWithLevel((int)userid);
                if (user != null && Usertarget != null)
                {
                    message.RecipientMessage = Usertarget;
                    message.UserMessage = user;
                    message.CreatedAt = DateTime.Now;
                    message.UpdatedAt = DateTime.Now;
                    _context.Add(message);
                    _context.SaveChanges();
                    return RedirectToAction("Show", "User", new { id = id });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                CommentsMessagesViewModel? commentsMessagesViewModel = ViewModel(id, message);
                if (commentsMessagesViewModel == null)
                {
                    return RedirectToAction("Show", new { id = id });
                }
                else
                {
                    return View("Show", commentsMessagesViewModel);
                }
            }
        }

        [SessionCheck]
        [HttpPost("users/{target}/messages/{id}/destroy")]
        public IActionResult DeleteMessage(int id, int target)
        {
            int? userid = HttpContext.Session.GetInt32("UserId");
            if (userid != null)
            {
                Message? message = GetMessage(id, target, userid);

                if (message != null)
                {
                    List<Comment> comments = GetAllCommentsbyMessageId(message);
                    if (comments.Any())
                    {
                        _context.Comments.RemoveRange(comments);
                    }
                    _context.Messages.Remove(message);
                    _context.SaveChanges();
                }
                return RedirectToAction("Show", new { id = target });
            }

            return RedirectToAction("Show", new { id = target });
        }

        [SessionCheck]
        [HttpPost("users/{target}/messages/{id}/comments/create")]
        public IActionResult CreateComment(
            int target,
            int id,
            [Bind(Prefix = "NewComment")] Comment comment
        )
        {
            int? userid = HttpContext.Session.GetInt32("UserId");
            if (ModelState.IsValid && userid != null)
            {
                User? user = _context.Users.Find(userid);
                Message? message = _context.Messages.Find(id);
                if (user != null && message != null)
                {
                    comment.UserComment = user;
                    comment.Message = message;
                    comment.CreatedAt = DateTime.Now;
                    comment.UpdatedAt = DateTime.Now;
                    _context.Add(comment);
                    _context.SaveChanges();
                }
                return RedirectToAction("Show", new { id = target });
            }
            else
            {
                List<Message>? modelmessage = ListMessages(target);
                User? user = FindUserWithLevel(target);
                CommentsMessagesViewModel commentsMessagesViewModel =
                    new CommentsMessagesViewModel();
                commentsMessagesViewModel.User = user;
                commentsMessagesViewModel.Messages = modelmessage;
                commentsMessagesViewModel.NewComment = comment;

                return View("Show", commentsMessagesViewModel);
            }
        }

        [SessionCheck]
        [HttpPost("users/{target}/messages/{id}/comments/{commentid}")]
        public IActionResult DeleteComment(int target, int id, int commentid)
        {
            int? userid = HttpContext.Session.GetInt32("UserId");
            if (userid != null)
            {
                Comment? comment = _context.Comments.FirstOrDefault(
                    a =>
                        a.CommentId == commentid
                        && a.UserComment.Id == userid
                        && a.Message.MessageId == id
                );
                if (comment != null)
                {
                    _context.Comments.Remove(comment);
                    _context.SaveChanges();
                }
                return RedirectToAction("Show", new { id = target });
            }
            return RedirectToAction("Show", new { id = target });
        }

        private CommentsMessagesViewModel? ViewModel(int id, Message message)
        {
            User? user = FindUserWithLevel(id);
            CommentsMessagesViewModel commentsMessagesViewModel = new CommentsMessagesViewModel();
            if (user != null)
            {
                List<Message>? modelmessage = ListMessages(id);
                if (modelmessage != null)
                {
                    commentsMessagesViewModel.Messages = modelmessage;
                    commentsMessagesViewModel.Newmessage = message;
                    return commentsMessagesViewModel;
                }
            }
            return null;
        }

        private List<Message>? ListMessages(int id)
        {
            return _context.Messages
                .Include(a => a.UserMessage)
                .Include(a => a.Comments)
                .Where(a => a.RecipientMessage.Id == id)
                .OrderByDescending(a => a.CreatedAt)
                .ToList();
        }

        private List<Comment> GetAllCommentsbyMessageId(Message? message)
        {
            return _context.Comments.Where(a => a.Message.MessageId == message.MessageId).ToList();
        }

        private Message? GetMessage(int id, int target, int? userid)
        {
            return _context.Messages.FirstOrDefault(
                a =>
                    a.MessageId == id
                    && a.UserMessage.Id == userid
                    && a.RecipientMessage.Id == target
            );
        }

        private User? FindUserWithLevel(int id)
        {
            return _context.Users.Include(a => a.UserLevel).FirstOrDefault(a => a.Id == id);
        }

        private User? FindUser(int id)
        {
            return _context.Users.FirstOrDefault(a => a.Id == id);
        }
    }
}
