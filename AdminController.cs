using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solar_Panel.Data;
using Solar_Panel.Models;

namespace Solar_Panel.Controllers
{
    public class AdminController : Controller
    {



        private readonly SolarPanelContext dbconn;

        public AdminController(SolarPanelContext conn)
        {
            dbconn = conn;
        }
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Index(string username, string password)
        {

            if (username == "Admin" && password == "admin")
            {
                return RedirectToAction("IndexNew", "Admin");
            }
            

            return View();
        }



        public IActionResult IndexNew()
        {
            return View();
        }

        


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public IActionResult Login(string username, string password)
        //{

        //    if (username == "Admin" && password == "Admin")
        //    {
        //        return RedirectToAction("Admin", "indexNew");
        //    }

        //    return View();
        //}

    }
}
