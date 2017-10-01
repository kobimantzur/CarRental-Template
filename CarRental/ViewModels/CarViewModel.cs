using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public class CarViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ManufactureId { get; set; }
        public string ManufactureName { get; set; }
        public string Image { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public int PricePerDay { get; set; }
        public bool isAvailable { get; set; }
        public bool isRecommendedCar { get; set; }
        public CarViewModel()
        {

        }
        public CarViewModel(Model m1,Manufacture m2)
        {
            this.ID = m1.ID;
            this.Name = m1.Name;
            this.ManufactureId = m1.ManufactureId;
            this.ManufactureName = m2.Name;
            this.Image = m1.Image;
            this.Year = m1.Year;
            this.Color = m1.Color;
            this.PricePerDay = m1.PricePerDay;
            this.isAvailable = m1.isAvailable;
        }

    }
}