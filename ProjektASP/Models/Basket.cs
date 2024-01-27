using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjektASP.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual ApplicationUser User { get; set; }
    }
}