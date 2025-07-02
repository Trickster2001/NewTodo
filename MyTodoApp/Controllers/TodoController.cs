using Microsoft.AspNetCore.Mvc;
using MyTodoApp.Data;
using MyTodoApp.Models;

namespace MyTodoApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly MyTodoAppContext _context;
        public TodoController(MyTodoAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }
            var todoList = _context.todos.Where(t => t.UserId == userId).ToList();
            return View(todoList);
        }

        [HttpGet]
        public IActionResult Filter(TodoStatus? status)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "User");

            var todoList = _context.todos.Where(t => t.UserId == userId);

            if (status.HasValue)
            {
                Console.WriteLine(status);
                //if (status == "-- All --")
                //{
                //    Console.WriteLine("All todos");
                //}
                todoList = todoList.Where(t => t.Status == status.Value);
            }

            return View("Index", todoList.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title, Description, IsCompleted, DueDate")] Todo todo)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (ModelState.IsValid)
            {
                todo.UserId = userId;
                Console.WriteLine("Todo is " + todo.UserId);
                _context.todos.Add(todo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(todo);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var todo = _context.todos.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Title, Description, Status")] Todo todo)
        {
            var existingTodo = _context.todos.Find(id);
            if(existingTodo == null)
            {
                return NotFound();
            }
            existingTodo.Title = todo.Title;
            existingTodo.Description = todo.Description;
            existingTodo.Status = todo.Status;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var todo = _context.todos.Find(id);
            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm (int id)
        {
            var todo = _context.todos.Find(id);
            if(todo != null)
            {
                _context.todos.Remove(todo);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Search(string query)
        {
            Console.WriteLine("Query is " + query);
            var userId = HttpContext.Session.GetInt32("UserId");

            var todoList = _context.todos.Where(t => t.UserId == userId && t.Title.Contains(query) || t.Description.Contains(query)).ToList();

            return View(todoList);
        }

        [HttpGet]
        public IActionResult Details (int id)
        {
            var todo = _context.todos.Find(id);
            return View(todo);
        }

        [HttpGet]
        public IActionResult AdminIndex()
        {
            return View();
        }
    }
}
