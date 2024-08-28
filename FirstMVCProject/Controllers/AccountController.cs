using FirstMVCProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Text;

namespace FirstMVCProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly CoreDbContext db;
        public AccountController(CoreDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var data=HttpContext.Session.GetString("User");
            if (data==null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.username = data;
                return View();
            }
            
        }

        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] data=ASCIIEncoding.ASCII.GetBytes(password);
                string ep= Convert.ToBase64String(data);
                return ep;
            }
        }

        public static string DecryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] data=Convert.FromBase64String(password);
                string dp=ASCIIEncoding.ASCII.GetString(data);
                return dp;
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User u)
        {
            if (ModelState.IsValid)
            {
                var us = new User()
                {
                    Name = u.Name,
                    Email = u.Email,
                    Password = EncryptPassword(u.Password)
                };
                db.users.Add(us);
                db.SaveChanges();
                TempData["msg"] = "User Added Successfully";
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
            
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel m)
        {
            if (ModelState.IsValid)
            {
                var data=db.users.Where(t=>t.Email.Equals(m.Email)).SingleOrDefault();
                if (data!=null)
                {
                    bool d=data.Email.Equals(m.Email) && DecryptPassword(data.Password).Equals(m.Password);
                    if(d)
                    {
                        HttpContext.Session.SetString("User", data.Email);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["InvalidPass"] = "Invalid Password!!";
                        return View();
                    }
                }
                else
                {
                    TempData["InvalidEmail"] = "Invalid Email Address!!";
                    return View();
                }
            }
            else
            {
                return View();
            }
            
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");          
        }
        
    }
}
