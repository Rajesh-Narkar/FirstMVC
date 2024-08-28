using FirstMVCProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FirstMVCProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly CoreDbContext db;

        public HomeController(CoreDbContext db)
        {
            this.db=db;
        }

        public IActionResult Index()
        {
            var data=db.Emps.ToList();
            return new JsonResult(data);
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
