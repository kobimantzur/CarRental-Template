using CarRental.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarRental.ViewModels
{
  
    public class RentalViewModel 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CarID { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Color { get; set; }
        public string ModelName { get; set; }
        public int PricePerDay { get; set; }
        public RentalViewModel()
        {

        }
        public RentalViewModel(Rental r,Model m)
        {
            this.ID = r.ID;
            this.CarID = r.CarID;
            this.Color = r.Color;
            this.PickupDate = r.PickupDate;
            this.ModelName = m.Name;
            this.ReturnDate = r.ReturnDate;
            this.PricePerDay = m.PricePerDay;
        }
    }
}