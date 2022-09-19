using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication12.Models
{
    public class urunler
    {
        [Key]
        public int urunID { get; set; }
        [Required(ErrorMessage = "mutlaka isim giriniz")]
        [Display(Name = "ürün İsimi giriniz")]
        [StringLength(20, ErrorMessage = "20 karakteri geçmeyin")]
        public string urunismi { get; set; }
        [Required(ErrorMessage = "mutlaka en giriniz")]
        [Display(Name = "en giriniz")]
        [Range(1, 10000000, ErrorMessage = "7 Basamaktan Daha Fazla Büyük Sayı Giremezsiniz")]
        public int en { get; set; }
        [Required(ErrorMessage = "mutlaka boy giriniz")]
        [Display(Name = "boy giriniz")]
        [Range(1,1000000, ErrorMessage = "7 Basamaktan Daha Fazla Büyük Sayı Giremezsiniz")]
        public int boy { get; set; }
        [Required(ErrorMessage = "mutlaka adet giriniz")]
        [Display(Name = "adet giriniz")]
        [Range(1,1000000, ErrorMessage = "7 Basamaktan Daha Fazla Büyük Sayı Giremezsiniz")]
        public int adet { get; set; }
        [Required(ErrorMessage = "mutlaka fihyat giriniz")]
        [Display(Name = "fiyat giriniz")]
        [Range(1,1000000000, ErrorMessage = "10 Basamaktan Daha Fazla Büyük Sayı Giremezsiniz")]
        public int fiyat { get; set; }
        [Required(ErrorMessage = "mutlaka renk giriniz")]
        [Display(Name = "renk giriniz")]
        public string renk { get; set; }
        public int kategoriID { get; set; }
        [Required(ErrorMessage = "mutlaka urunkodu giriniz")]
        [Display(Name = "urunkodu giriniz")]
        [Range(1, 100000, ErrorMessage = "8 Basamaktan Daha Fazla Büyük Sayı Giremezsiniz")]
        public int urunkodu { get; set; }
        public int toplam { get; set; }
        public string resim { get; set; }

        public virtual Kategori Kategori { get; set; }
    }
}