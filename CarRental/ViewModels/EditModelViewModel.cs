using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.ViewModels
{
    public class EditModelViewModel
    {
        public List<Manufacture> ManufacturesList { get; set; }
        public Model Model { get; set; }
    }
}