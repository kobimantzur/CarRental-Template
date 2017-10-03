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
        //Input - pickup&return dates, carid, userId
        //Output - success or failure
        [HttpPost]
        public ActionResult Rent()
        {
            string status = String.Empty;
            try { 
            Rental r = new Rental();
            r.PickupDate = DateTime.ParseExact(Request.Form["pickupDate"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            r.ReturnDate = DateTime.ParseExact(Request.Form["returnDate"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            r.CarID = int.Parse(Request.Form["CarID"]);
            r.DriverAge = 21;
            r.UserID = int.Parse(Request.Form["UserID"]);
            dbContext.Rental.Add(r);
            dbContext.SaveChanges();
                status = "success";
            }
            catch(Exception ex)
            {
                status = "failure";
            }
            return Content(status, "application/json");

        }
        //Input - the facebook App ID
        //Output - the user object
        [HttpGet]
        public ActionResult GetLoggedUser(string userId)
        {
            User user = dbContext.User.Where(x => x.AppID == userId).FirstOrDefault();

            return Content(JsonConvert.SerializeObject(user), "application/json");
        }
        //Input - the user's facebook app id
        //Output - the user object
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