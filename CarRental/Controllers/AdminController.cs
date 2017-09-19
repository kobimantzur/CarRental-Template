using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Controllers
{
    public class AdminController : Controller
    {
        CarRentalContext dbContext = new CarRentalContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddManufacture()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddManufacture(Manufacture m)
        {
            try
            {
                Manufacture temp = dbContext.Manufacture.Where(x => x.Name.Contains(m.Name)).FirstOrDefault();
                if (temp == null)
                {
                    dbContext.Manufacture.Add(m);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult AddModel()
        {
            List<Manufacture> manufacturesList = new List<Manufacture>();
            manufacturesList = dbContext.Manufacture.OrderBy(x => x.Name).ToList();
            return View(manufacturesList);
        }
        [HttpPost]
        public ActionResult AddModel(Model m)
        {
            try
            {
                m.isAvailable = false;
                dbContext.Model.Add(m);
                dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Manage()
        {

            return View();
        }
        public ActionResult Statistics()
        {

            return View();
        }


    }
}