using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Solar_Panel.Data;
using Solar_Panel.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Solar_Panel.Controllers
{
    public class HomeController : Controller
    {
        private readonly SolarPanelContext dbconn;

        public HomeController(SolarPanelContext db)
        {
            dbconn = db;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.session = HttpContext.Session.GetString("name");
            }
            return View();
        }

        public IActionResult Package()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.session = HttpContext.Session.GetString("name");
            }
            return View(dbconn.Servicees.ToList());

        }

        public IActionResult Booking()
        {

            ViewData["SId"] = new SelectList(dbconn.Servicees, "SId", "SId");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Booking(Booking book)
        {
            if (ModelState.IsValid)
            {
                dbconn.Bookings.Add(book);
                await dbconn.SaveChangesAsync();
                return RedirectToAction("Index");
                 //ViewBag.msg = "Booking successfully";
            }

            ViewBag.msg = "Invalid Email and/or Password";
            return View();
        }




        public IActionResult Shop()
        {
            if(HttpContext.Session.GetString("name") != null)
            {
                ViewBag.session = HttpContext.Session.GetString("name");
            }
            return View(dbconn.Products.ToList());
          
        }



        public IActionResult Addtocart(int? Id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.session = HttpContext.Session.GetString("name");
            }


            List<Cart> cartItems = HttpContext.Session.GetObject<List<Cart>>("Sess_Name") ?? new List<Cart>();

            Cart exisitng_item = cartItems.Find(item => item.ProductId == Id);
            if (exisitng_item != null)
            {
                exisitng_item.Quantity += 1;
            }
            else
            {
                var mydata = dbconn.Products.Find(Id);
                cartItems.Add(
                new Cart
                {
                    ProductId = mydata.PId,
                    ProductName = mydata.Pname,
                    Quantity = 1,
                    Price = Convert.ToInt32(mydata.Pprice)
                }
                    );
            }
            HttpContext.Session.SetObject<List<Cart>>("Sess_Name", cartItems);
            ViewBag.mycart = HttpContext.Session.GetObject<List<Cart>>("Sess_Name");
            return View();

        }

        public IActionResult minus(int? Id)
        {
            List<Cart> cartItems = HttpContext.Session.GetObject<List<Cart>>("Sess_Name") ?? new List<Cart>();
            Cart exisitng_item = cartItems.Find(item => item.ProductId == Id);
            if (exisitng_item != null)
            {
                if (exisitng_item.Quantity > 1)
                {
                    exisitng_item.Quantity -= 1;
                }
                else if (exisitng_item.Quantity == 1)
                {
                    cartItems.Remove(exisitng_item);

                }
            }
            HttpContext.Session.SetObject<List<Cart>>("Sess_Name", cartItems);
            ViewBag.mycart = HttpContext.Session.GetObject<List<Cart>>("Sess_Name");
            return View("Addtocart");
        }



        public IActionResult plus(int? Id)
        {
            List<Cart> cartItems = HttpContext.Session.GetObject<List<Cart>>("Sess_Name") ?? new List<Cart>();
            Cart exisitng_item = cartItems.Find(item => item.ProductId == Id);
            
            if (exisitng_item != null)
            {
                if (exisitng_item.Quantity >= 1)
                {
                    exisitng_item.Quantity += 1;
                }
                
            }
            HttpContext.Session.SetObject<List<Cart>>("Sess_Name", cartItems);
            ViewBag.mycart = HttpContext.Session.GetObject<List<Cart>>("Sess_Name");
            return View("Addtocart");
        }







        public IActionResult checkout(int total)
        {
            TempData["p"] = total;
            TempData.Keep();
            return View();
        }

        [HttpPost]
        public IActionResult checkout(IFormCollection f)
        {
            List<Cart> cartItems = HttpContext.Session.GetObject<List<Cart>>("Sess_Name") ?? new List<Cart>();

            //insertion in order table
            Order o = new Order();
            o.Dates = DateTime.Now.ToShortDateString();
            o.TPrice = int.Parse(TempData["p"].ToString());
            o.Status = "Pending";
            o.Address = f["add"];
            o.Message = f["msg"];
            o.Contact = f["con"];
            o.UId = int.Parse(HttpContext.Session.GetString("id"));

            dbconn.Add(o);
            dbconn.SaveChanges();

            foreach (Cart itms in cartItems)
            {
                //insertion in item table
                OrderItem itemsTable = new OrderItem();
                itemsTable.PId = itms.ProductId;
                itemsTable.Qty = itms.Quantity;
                itemsTable.OId = o.OId;
                dbconn.Add(itemsTable);
                dbconn.SaveChanges();

            }

            HttpContext.Session.SetObject("Sess_Name", "");
            return RedirectToAction(nameof(Index));

        }












        public IActionResult About()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.session = HttpContext.Session.GetString("name");
            }
            return View();
        }

        public IActionResult Contact()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.session = HttpContext.Session.GetString("name");
            }
            return View();
        }

        public async Task<IActionResult> Myorder(int? id)
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.session = HttpContext.Session.GetString("name");
            }

            if (id == null)
            {
                return NotFound();
            }

            var order = dbconn.Orders.Include(x => x.UIdNavigation).Where(x => x.UId == id);

            if (order == null)
            {
                return NotFound();
            }
           
            return View(order.ToList());

        }



        public IActionResult SignUp()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.session = HttpContext.Session.GetString("name");
            }

            return View();
        }

        [HttpPost]
        public  async Task<IActionResult> SignUp(User user)
        {
            dbconn.Users.Add(user);
            await dbconn.SaveChangesAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Login(string returnUrl)
        {
            if(!string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnUrl = returnUrl;
            }
            return View();
        }

        public IActionResult Logout(string returnUrl)
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }



        [HttpPost]
        public IActionResult Login(User us, string returnUrl)
        {

            // var data = dbConn.Users.Where(db => db.Email == us.Email && db.Password == us.Password).SingleOrDefaultAsync();


            var data = (from dt in dbconn.Users where dt.Email == us.Email && dt.Upass == us.Upass select dt).SingleOrDefault();


            if (data != null)
            {
                HttpContext.Session.SetString("name", data.Uname);
                HttpContext.Session.SetString("id", data.UId.ToString());
                // HttpContext.Session.SetString("id", data.Id.ToString());

                if(!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index");
                }

               
            }
            else
            {

                ViewBag.msg = "Invalid Email and/or Password";
                return View();
            }
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


    //SessionExtensios Class

    public static class SessionExtensions
    {

        public static void SetObject<T>(this ISession session, string key, T value)
        {
            string serializedValue = JsonConvert.SerializeObject(value);
            session.SetString(key, serializedValue);
        }
        public static T GetObject<T>(this ISession session, string key)
        {
            string serializedValue = session.GetString(key);
            if (serializedValue != null)
            {
                return JsonConvert.DeserializeObject<T>(serializedValue);
            }
            return default(T);
        }

    }






}