using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektASP.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; } // Identyfikator użytkownika, który złożył zamówienie
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual ICollection<Product> OrderProducts { get; set; }
    }
}