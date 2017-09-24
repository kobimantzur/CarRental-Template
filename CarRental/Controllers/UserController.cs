using CarRental.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Controllers
{
    public class UserController : Controller
    {
        CarRentalContext dbContext = new CarRentalContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Rent()
        {
            Rental r = new Rental();
            r.PickupDate = DateTime.ParseExact(Request.Form["pickupDate"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            r.ReturnDate = DateTime.ParseExact(Request.Form["returnDate"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            r.CarID = int.Parse(Request.Form["CarID"]);
            r.DriverAge = 21;
            r.UserID = int.Parse(Request.Form["UserID"]);
            dbContext.Rental.Add(r);
            dbContext.SaveChanges();
            return Content("success", "application/json");
        }

        [HttpGet]
        public ActionResult GetLoggedUser(string userId)
        {
            User user = dbContext.User.Where(x => x.AppID == userId).FirstOrDefault();

            return Content(JsonConvert.SerializeObject(user), "application/json");
        }
        [HttpPost]
        public ActionResult Login()
        {
            
            User user = new Models.User();
            string userId = Request.Form["userId"];
            user = dbContext.User.Where(x => x.AppID == userId).FirstOrDefault();
            if (user == null)
            {
                user = new Models.User();
                user.AppID = Request.Form["userId"];
                user.Email = Request.Form["email"];
                user.FullName = Request.Form["fullName"];
                user.Role = "client";
                dbContext.User.Add(user);
                dbContext.SaveChanges();
                user = dbContext.User.Where(x => x.AppID == userId).FirstOrDefault();
            }
            return Content(JsonConvert.SerializeObject(user), "application/json");
        }

    }
}