using CarRental.Models;
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


 
    }
}