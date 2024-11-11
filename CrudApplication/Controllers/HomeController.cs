using CrudApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CrudApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbcontext ;//Dependency Injection const
        //immutable -> not changeable
        public HomeController(ApplicationDbContext dbContext)//constructor injection
        {
           _dbcontext = dbContext;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]  //Http Protocols
        public IActionResult AddStudent()
        {
            return View();//view GEt
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//CSRF Cross Site Request Foregery token
        public IActionResult AddStudent(Student student)//method overloading
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Students.Add(student);//LINQ Language integrated Query
                _dbcontext.SaveChanges();
                TempData["success"] = "Student Registered Successfully";
                return RedirectToAction("ViewStudent","Home");
            }
            return View();//view GEt
        }
        [HttpGet]
        public IActionResult ViewStudent()
        {
            var students = _dbcontext.Students.ToList();
            return View(students);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var student = _dbcontext.Students.FirstOrDefault(x=>x.Id==id);
            return View(student);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _dbcontext.Students.Find(id);
            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//CSRF Cross Site Request Foregery token
        public IActionResult Edit(Student student)//method overloading
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Students.Update(student);//LINQ Language integrated Query
                _dbcontext.SaveChanges();
                TempData["update"] = "Student Updated Successfully";
                return RedirectToAction("ViewStudent", "Home");
            }
            return View();//view GEt
        }
        [HttpGet]
        public IActionResult Delete(int id)//Are you sure you want to delete
        {
            var student = _dbcontext.Students.Find(id);
            return View(student);
        }
        [HttpPost]
        
        public IActionResult Delete(Student student)//method overloading
        {
           
                _dbcontext.Students.Remove(student);//LINQ Language integrated Query
                _dbcontext.SaveChanges();
                TempData["delete"] = "Student Deleted Successfully";
                return RedirectToAction("ViewStudent", "Home");
           
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
