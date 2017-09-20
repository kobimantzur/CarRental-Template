using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.ViewModels
{
    public class SearchViewModel
    {
        public DateTime PickupDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxPricePerDay { get; set; }

    }
}