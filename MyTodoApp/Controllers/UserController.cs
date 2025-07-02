using Microsoft.AspNetCore.Mvc;
using MyTodoApp.Data;
using MyTodoApp.Models;

namespace MyTodoApp.Controllers
{
    public class UserController : Controller
    {
        private readonly MyTodoAppContext _context;

        public UserController(MyTodoAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register ()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("Username, Password")] User user)
        {
            if(ModelState.IsValid)
            {
                _context.users.Add(user);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("username", user.Username);
                return RedirectToAction("Index", "Todo");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Login ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([Bind("Username, Password")] User user)
        {
            var existingUser = _context.users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            //Console.WriteLine("Existing user id is " + existingUser.UserId);
            //Console.WriteLine("user id is" + user.UserId);
            Console.WriteLine($"Admin: {existingUser.IsAdmin}");

            if (existingUser.IsAdmin)
            {
                HttpContext.Session.SetInt32("UserId", existingUser.UserId);
                HttpContext.Session.SetInt32("admin", existingUser.IsAdmin ? 1 : 0);
                return RedirectToAction("AdminIndex", "Todo");
            }

            if(existingUser != null)
            {
                HttpContext.Session.SetInt32("UserId", existingUser.UserId);
                HttpContext.Session.SetString("username", existingUser.Username);
                return RedirectToAction("Index", "Todo");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(user);
            }
        }

        [HttpGet]
        public IActionResult Logout ()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("username");
            return RedirectToAction("Login");
        }
    }
}
