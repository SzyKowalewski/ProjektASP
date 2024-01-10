﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektASP.Models
{
    public class AddressModels
    {
        public int Id { get; set; }
        [MaxLength(6)]
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string StreetAndBuildingNumber { get; set; }
        public int ApartmentNumber { get; set; }
    }
}