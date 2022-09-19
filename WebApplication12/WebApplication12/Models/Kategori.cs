using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication12.Models
{
    public class Kategori
    {
        [Key]
        public int kategoriID { get; set; }
        public string katagoriismi { get; set; }
        public string resim { get; set; }
        public virtual ICollection<urunler> Urunlers { get; set; }
    }
}