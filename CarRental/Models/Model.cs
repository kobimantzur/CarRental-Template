﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public class Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        [ForeignKey("Manufacture")]
        public int ManufactureId { get; set; }
        public string Image { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public int PricePerDay { get; set; }
        public bool isAvailable { get; set; }
        public virtual Manufacture Manufacture { get; set; }

    }
}