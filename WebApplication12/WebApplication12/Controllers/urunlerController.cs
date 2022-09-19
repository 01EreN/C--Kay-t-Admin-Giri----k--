using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication12.Models;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace WebApplication12.Controllers
{
    public class urunlerController : Controller
    {



        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string GridHtml)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Grid.pdf");
            }
        }





        // GET: urunler
        context dbbaglan = new context();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult goster_ici()
        {
            var listele_goster = dbbaglan.kategoris.ToList();
            return PartialView(listele_goster);

        }

        public string User_Name = string.Empty;
        string User_sifre = string.Empty;
        string User_yetki = string.Empty;
        string User_ok = string.Empty;
        public ActionResult goster(int? id)
        {
            try
            {

                HttpCookie reqCookies = Request.Cookies["userInfo"];
                if (reqCookies != null)
                {
                    User_Name = reqCookies["UserName"].ToString();
                    User_sifre = reqCookies["Usersifre"].ToString();
                    User_yetki = reqCookies["Useryetki"].ToString();
                    User_ok = reqCookies["Userok"].ToString();
                }
                else
                {
                    User_Name = "";
                    User_sifre = "";
                    User_yetki = "";
                    User_ok = "";
                    return RedirectToAction("login");
                }
            }
            catch (Exception)
            {

                return RedirectToAction("login");
            }
            if (id != null)
            {
                var calisanbilgisigetir = (from i in dbbaglan.urunlers
                                           where i.kategoriID == id
                                           select i).ToList();
                return View(calisanbilgisigetir);
            }
            return View();

        }



        sepet yenisepet = new sepet();
        public ActionResult sepet_ekle(int id, string adet)
        {
            var gelen = dbbaglan.urunlers.Find(id);
            yenisepet.urunismi = gelen.urunismi;
            yenisepet.fiyat = gelen.fiyat;
            yenisepet.en = gelen.en;
            yenisepet.boy = gelen.boy;
            yenisepet.urunkodu = gelen.urunkodu;
            yenisepet.renk = gelen.renk;
            yenisepet.resim = gelen.resim;

            yenisepet.adet = Convert.ToInt32(adet);
            yenisepet.kategoriID = gelen.kategoriID;
            sepetlist.newsepet.Add(yenisepet);
            int git = gelen.kategoriID;
            return RedirectToAction("goster", new { id = git });
        }
        public PartialViewResult sepet_ici()
        {
            var gonder = sepetlist.newsepet.ToList();
            foreach (var item in gonder)
            {
                item.toplam = item.adet * item.fiyat;

            }
            int topla = (from i in gonder
                         select i.toplam).Sum();
            ViewBag.toplam = topla;

            return PartialView(gonder);
        }
        public ActionResult sepet_sil(int id)
        {
            var sil = (from i in sepetlist.newsepet
                       where i.kategoriID == id
                       select i).FirstOrDefault();
            sepetlist.newsepet.Remove(sil);
            return View("goster");
        }







        [HttpGet]
        public ActionResult urungir()
        {
            try
            {

                HttpCookie reqCookies = Request.Cookies["userInfo"];
                if (reqCookies != null)
                {
                    User_Name = reqCookies["UserName"].ToString();
                    User_sifre = reqCookies["Usersifre"].ToString();
                    User_yetki = reqCookies["Useryetki"].ToString();
                    User_ok = reqCookies["Userok"].ToString();
                }
                else
                {
                    User_Name = "";
                    User_sifre = "";
                    User_yetki = "";
                    User_ok = "";
                    return RedirectToAction("login");
                }
            }
            catch (Exception)
            {

                return RedirectToAction("login");
            }
            if (User_yetki == "user")
            {
                return RedirectToAction("index", "home");
            }

            var uruns = dbbaglan.kategoris.ToList();
            ViewBag.uruncanta = new SelectList(uruns, "kategoriID", "katagoriismi");
            return View();
        }


        public void dropveri()
        {
            var uruns = dbbaglan.kategoris.ToList();
            ViewBag.uruncanta = new SelectList(uruns, "kategoriID", "katagoriismi");
        }


        Random rndm = new Random();
        urunler urun = new urunler();
        [HttpPost]
        public ActionResult urungir(urunler yeniurun, HttpPostedFileBase resim, string urunveri)
        {

           
            if (!String.IsNullOrEmpty(urunveri))
            {
                yeniurun.kategoriID = int.Parse(urunveri);
            }
            else
            {
                dropveri();
                ViewBag.mesajim = "Lütfen Katagori Giriniz 24";
                return View(yeniurun);
            }
                
         

            if (!ModelState.IsValid)
            {
                ViewBag.mesajim = "Eksiksiz formu doldurun";

                dropveri();

                return View(yeniurun);
            }

            if (resim != null)
            {
                string resimisim;

                do
                {
                    double randomuretilen = rndm.Next(1, 2147483647);
                    resimisim = "_" + randomuretilen + resim.FileName;
                    var varmi = (from i in dbbaglan.urunlers
                                 where i.resim == resimisim
                                 select i).ToList();
                    if (varmi.Count == 0)
                    {
                        urun.resim = resimisim;
                        break;
                    }
                } while (true);

                resim.SaveAs(Server.MapPath("~/img/") + resimisim);

                dbbaglan.urunlers.Add(yeniurun);
                dbbaglan.SaveChanges();
                ViewBag.mesajim = "Kayıt Başarılı";

                dropveri();

                return View();

            }
            else
            {
                ViewBag.mesajim = "Resim giriniz";

                dropveri();

                ViewBag.test = "buraya geldi";
                return View(yeniurun);
            }



        }



        public ActionResult logincikis()
        {
            if (Request.Cookies["userInfo"] != null)
            {
                HttpCookie CEREZ = new HttpCookie("userInfo");
                CEREZ.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(CEREZ);
            }
            return RedirectToAction("login");

        }
        [HttpGet]
        public ActionResult login()
        {

            return View();

        }
        [HttpPost]
        public ActionResult login(string isim, string sifre)
        {
            //sqlden sorgu yap
            var varmi = (from i in dbbaglan.girisler
                         where i.isim == isim && i.sifre == sifre
                         select i).Count();
            if (varmi > 0)
            {
                // cooki işlemleri
                ViewBag.mesaj = "Giriş başarılı";

                string yetki1 = (from i in dbbaglan.girisler
                                 where i.isim == isim && i.sifre == sifre
                                 select i.yetki).FirstOrDefault();

                HttpCookie userInfo = new HttpCookie("userInfo");
                userInfo["UserName"] = isim;
                userInfo["Usersifre"] = sifre;
                userInfo["Useryetki"] = yetki1;
                userInfo["Userok"] = "ok";
                DateTime now = DateTime.Now;
                userInfo.Expires = now.AddMinutes(3);
                Response.Cookies.Add(userInfo);

            }
            else
            {
                ViewBag.mesaj = "Giriş başarısız";
            }

            return View();

        }





    }
}