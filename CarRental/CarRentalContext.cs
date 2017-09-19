using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CarRental
{
    public class CarRentalContext : DbContext
    {
        public CarRentalContext()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Manufacture> Manufacture { get; set; }

        public DbSet<Model> Model { get; set; }
        public DbSet<Rental> Rental { get; set; }
        public DbSet<User> User { get; set; }

    }
}