using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication12.Models
{
    public class sepetlist
    {
        public static List<sepet> newsepet = new List<sepet>();


        public static string User_Name { get; set; }
        public static string User_sifre { get; set; }
        public static string User_yetki { get; set; }
        public static string User_ok { get; set; }
    }
}