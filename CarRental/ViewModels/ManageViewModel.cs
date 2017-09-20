using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.ViewModels
{
    public class ManageViewModel
    {
        public List<Manufacture> ManufacturesList { get; set; }
        public List<Model> ModelsList { get; set; }
    }
}