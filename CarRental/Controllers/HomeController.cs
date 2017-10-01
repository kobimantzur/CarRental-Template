using CarRental.Models;
using CarRental.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            try
            {
                modelsList = dbContext.Model.ToList();
                var vmList = modelsList.GroupJoin(dbContext.Manufacture, model => model.ManufactureId,
                manufacture => manufacture.ID,
                (x, y) => new CarViewModel(x, y.FirstOrDefault()));
              
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
                CarViewModel vm = new CarViewModel();
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

        public ActionResult SearchResults(string PickupDate, string ReturnDate,int ManufactureId = -1)
        {
            DateTime parsedPickupDate = DateTime.ParseExact(PickupDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime parsedReturnDate = DateTime.ParseExact(ReturnDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var models = new List<Model>();
            models = dbContext.Model.Where(x => (dbContext.Rental.Where(y=> y.CarID == x.ID && (parsedPickupDate >= y.PickupDate && parsedReturnDate <= y.ReturnDate )).ToList().Count == 0)).ToList();
            if (ManufactureId != -1)
            {
                models = models.Where(x => x.ManufactureId == ManufactureId).ToList();
            }
            var vmList = models.GroupJoin(dbContext.Manufacture, model => model.ManufactureId,
                manufacture => manufacture.ID,
                (x, y) => new CarViewModel(x, y.FirstOrDefault()));

            return View(vmList.ToList<CarViewModel>());
        }

        public ActionResult VisitUs()
        {
            List<Dealership> dealershipsList = dbContext.Dealership.ToList();
            ViewBag.DealershipsList = JsonConvert.SerializeObject(dealershipsList);
            return View();
        }

    }
}