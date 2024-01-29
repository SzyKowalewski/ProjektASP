using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektASP.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isVisible { get; set; }
    }
}