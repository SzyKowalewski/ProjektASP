using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektASP.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public double Price { get; set; }
        public bool Avaliable { get; set; }
        public virtual ICollection<AttachedFile> AttachedFiles { get; set; }

        [NotMapped]
        [Display(Name = "Upload Image")]
        public HttpPostedFileBase ImageFile { get; set; }

        // Nowa kolekcja zdjęć
        public virtual ICollection<Image> Images { get; set; }
    }
}
