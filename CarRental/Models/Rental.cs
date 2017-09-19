using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public class Rental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public int ModelID { get; set; }
        public DateTime StartDate { get; set; }
        public int EndDate { get; set; }
        public string Color { get; set; }
        public int DriverAge { get; set; }

    }
}