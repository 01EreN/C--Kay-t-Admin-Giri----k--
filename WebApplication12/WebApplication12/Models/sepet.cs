using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication12.Models
{
    public class sepet
    {
        [Key]
        public int id { get; set; }
        public int urunID { get; set; }
        public string urunismi { get; set; }
        public int en { get; set; }
        public int boy { get; set; }
        public int adet { get; set; }
        public int fiyat { get; set; }
        public string renk { get; set; }
        public int kategoriID { get; set; }
        public int urunkodu { get; set; }
        public int toplam { get; set; }
        public string resim { get; set; }
    }
}