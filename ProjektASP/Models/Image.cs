using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektASP.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public int ProductId { get; set; }
        public string ImagePath { get; set; }
        public virtual Product Product { get; set; }
    }
}