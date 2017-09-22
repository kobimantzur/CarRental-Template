using CarRental.Models;
using CarRental.ViewModels;
using Newtonsoft.Json;
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
            if (dbContext.Manufacture.Count() > 0)
            {
                manufacturesList = dbContext.Manufacture.OrderBy(x => x.Name).ToList();
            }
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
            catch (Exception ex)
            {
                RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Manage()
        {
            ManageViewModel vm = new ManageViewModel();
            vm.ModelsList = dbContext.Model.ToList();
            vm.ManufacturesList = dbContext.Manufacture.ToList();

            return View(vm);
        }
        public ActionResult Statistics()
        {

            return View();
        }

        public ActionResult DeleteManufacture(int ID)
        {
            try
            {
                Manufacture m = dbContext.Manufacture.Where(x => x.ID == ID).FirstOrDefault();
                List<Model> models = dbContext.Model.Where(x => x.ManufactureId == ID).ToList();
                foreach (Model item in models)
                {
                    dbContext.Model.Remove(item);

                }
                dbContext.Manufacture.Remove(m);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Manage", "Admin");
        }

        [HttpGet]
        public ActionResult DeleteModel(int ID)
        {
            try
            {
                Model m = dbContext.Model.Where(x => x.ID == ID).FirstOrDefault();
                dbContext.Model.Remove(m);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Manage", "Admin");
        }
        [HttpGet]
        public ActionResult UpdateManufacture(int ID, string Name)
        {
            try
            {
                Manufacture m = dbContext.Manufacture.Where(x => x.ID == ID).FirstOrDefault();
                m.Name = Name;
                dbContext.Entry(m).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Manage", "Admin");
        }

        public ActionResult EditModel(int ID)
        {
            EditModelViewModel vm = new EditModelViewModel();
            try
            {
                vm.Model = dbContext.Model.Where(x => x.ID == ID).FirstOrDefault();
                vm.ManufacturesList = dbContext.Manufacture.OrderBy(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                RedirectToAction("Index", "Home");
            }
            return View(vm);
        }
        [HttpPost]
        public ActionResult EditModel(Model m)
        {
            try
            {
                dbContext.Entry(m).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Manage", "Admin");
        }

        [HttpGet]
        public ActionResult Search(DateTime PickupDate,DateTime endDate)
        {
            return View();
        }


    }
}