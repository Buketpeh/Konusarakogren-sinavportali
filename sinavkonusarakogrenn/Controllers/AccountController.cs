using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sinavkonusarakogrenn.Data;

namespace sinavkonusarakogrenn.Controllers
{
    public class AccountController : Controller
    {
        public readonly AppDbContext _dbContext;
        public AccountController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("Account/Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Account/Login")]
        public IActionResult Login(string username, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == username &&  u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("id", user.Id);
                HttpContext.Session.SetString("fullname", user.Name);
                return RedirectToAction("PostList", "Home");
            }
            else
            {
                ViewBag.Uyari = "Kullanıcı adı veya şifre hatalı";
                return View();
            }
            //return View("login");
        }
        [Route("Account/logout")]
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
