using CarRental.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public ActionResult Rent()
        {
            Rental r = new Rental();
            r.PickupDate = DateTime.Now;
            r.ReturnDate = r.PickupDate.AddDays(5);
            r.ModelID = 2;
            r.DriverAge = 21;
            dbContext.Rental.Add(r);
            dbContext.SaveChanges();
            return View();
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