using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication12.Models;

namespace WebApplication12.Controllers
{
    public class HomeController : Controller
    {
        login yeni = new login();
        context dbbaglan =new context();
        public ActionResult Index()
        {
            for (int i = 0; i < 100000; i++)
            {

            }
            var girislist = dbbaglan.girisler.ToList();
            if (girislist.Count < 1)
            {
                yeni.isim = "a";
                yeni.sifre = "a";
                yeni.yetki = "admin";
                dbbaglan.girisler.Add(yeni);
                dbbaglan.SaveChanges();
                yeni.isim = "u";
                yeni.sifre = "u";
                yeni.yetki = "user";
                dbbaglan.girisler.Add(yeni);
                dbbaglan.SaveChanges();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}