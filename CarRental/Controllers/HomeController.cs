using CarRental.Models;
using CarRental.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Controllers
{
    public class HomeController : Controller
    {
        CarRentalContext dbContext = new CarRentalContext();

        public ActionResult Index()
        {
            HomePageViewModel vm = new HomePageViewModel();
            vm.ManufacturesList = dbContext.Manufacture.OrderBy(x => x.Name).ToList();

            return View(vm);
        }

        public ActionResult All()
        {
            List<Model> modelsList = new List<Model>();
            List<AllViewModel> vmList = new List<AllViewModel>();
            try
            {
                modelsList = dbContext.Model.ToList();
                foreach (var item in modelsList)
                {
                    AllViewModel vm = new AllViewModel();
                    vm.ID = item.ID;
                    vm.Name = item.Name;
                    vm.Image = item.Image;
                    vm.ManufactureId = item.ManufactureId;
                    vm.Year = item.Year;
                    vm.PricePerDay = item.PricePerDay;
                    vm.ManufactureName = dbContext.Manufacture.Where(x => x.ID == item.ManufactureId).FirstOrDefault().Name;
                    vmList.Add(vm);
                }
                return View(vmList);
            }
            catch (Exception ex)
            {
                RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult ViewDetails(int ID)
        {
            try {
                Model model = dbContext.Model.Where(x => x.ID == ID).FirstOrDefault();
                AllViewModel vm = new AllViewModel();
                vm.ID = model.ID;
                vm.Name = model.Name;
                vm.isAvailable = model.isAvailable;
                vm.ManufactureId = model.ManufactureId;
                vm.PricePerDay = model.PricePerDay;
                vm.Image = model.Image;
                vm.Year = model.Year;
                vm.ManufactureName = dbContext.Manufacture.Where(x => x.ID == model.ManufactureId).FirstOrDefault().Name;
                return View(vm);
            }
            catch (Exception ex)
            {
                RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SearchResults(DateTime PickupDate, DateTime EndDate)
        {
            List<Model> models = new List<Model>();
            return View();
        }
    }
}