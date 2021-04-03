using System;
using System.Linq;
using System.Web.Mvc;
using TelefonRehberi.Dto.Account;
using TelefonRehberi.Entity;

namespace Telefon_Rehberi_MVC.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        [HttpGet]
        public ActionResult Profile(int id)
        {
            PersonelModel personel = new PersonelModel();
            personel.calisan = context.Calisanlar.FirstOrDefault(x => x.id == id);
            personel.adi = context.Departmanlar.FirstOrDefault(x => x.id == personel.calisan.departmanId).adi;
            Calisanlar yonetici = context.Calisanlar.FirstOrDefault(y => y.id == personel.calisan.yoneticiId);

            if (yonetici == null)
                personel.yoneticiAdSoyad = "Yönetici bilgisi yok!";
            else
                personel.yoneticiAdSoyad = yonetici.ad + " " + yonetici.soyad;
            return View(personel);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Session["LogonAdmin"] != null)
            {
                ChangePasswordModel model = new ChangePasswordModel();
         
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
       
            if(Session["LogonAdmin"] == null)
            {
                TempData["resultInfo"] = "Oturum süreniz dolmuştur. Lütfen Oturum Açıp Tekrar Deneyiniz!";
                return RedirectToAction("Login", "Action");
            }
            if (model.admin.password == model.password)
            {
                var admin = context.Admin.FirstOrDefault(x => x.username == logonUserName);
                if (admin != null)
                {
                    if (admin.password == model.currentPassword)//girilen mevcut şifre doğrumu
                    {
                        admin.password = model.admin.password;
                        try
                        {

                            context.Entry<Admin>(admin).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                            TempData["resultInfo"] = "Şifreniz Başarıyla Değiştirildi";

                        }
                        catch (Exception ex)
                        {
                            TempData["resultInfo"] = "Üzgünüz şifreniz değiştirilemedi!";

                        }
                        return View(new ChangePasswordModel());
                    }
                    else
                    {
                        TempData["resultInfo"] = "Mevcut Şifreniz Doğru Değil";
                        return View(new ChangePasswordModel());
                    }
                }
                else
                {
                    TempData["resultInfo"] = "Şifreniz Değiştirilemedi!";
                    return View(new ChangePasswordModel());
                }
            }
            else
            {
                TempData["resultInfo"] = "Şifreler Uyuşmamaktadır!";
                return View(new ChangePasswordModel());
            }
            
        }

        [HttpGet]
        public ActionResult ProfileCreate()
        {
            Calisanlar model = new Calisanlar();
            var departmanlar = context.Departmanlar.Select(x => new SelectListItem()
            {
                Text = x.adi,
                Value = x.id.ToString()
            }).ToList();
            ViewBag.departmanList = departmanlar;
            // model.departmanList = departmanlar;
            var yoneticiler = context.Calisanlar.Select(x => new SelectListItem()
            {
                Text = x.ad + " " + x.soyad,
                Value = x.id.ToString()
            }).ToList();
            ViewBag.yoneticiList = yoneticiler;
            return View(model);
        }

        [HttpPost]
        public ActionResult ProfileCreate(Calisanlar personel)
        {

            System.Diagnostics.Debug.WriteLine("SomeText");
            System.Diagnostics.Debug.WriteLine("ad" + personel.ad);
            System.Diagnostics.Debug.WriteLine("soyad" + personel.soyad);
            System.Diagnostics.Debug.WriteLine("tel" + personel.telefon);
            System.Diagnostics.Debug.WriteLine("yonetici" + personel.yoneticiId);
            System.Diagnostics.Debug.WriteLine("departman" + personel.departmanId);


       

            try
            {
                
                context.Calisanlar.Add(personel);
                context.SaveChanges();
                TempData["resultInfo"] = "Yeni Profil Kaydı Oluşturuldu.";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["resultInfo"] = ex.Message;
                var departmanlar = context.Departmanlar.Select(x => new SelectListItem()
                {
                    Text = x.adi,
                    Value = x.id.ToString()
                }).ToList();
                ViewBag.departmanList = departmanlar;
                // model.departmanList = departmanlar;
                var yoneticiler = context.Calisanlar.Select(x => new SelectListItem()
                {
                    Text = x.ad + " " + x.soyad,
                    Value = x.id.ToString()
                }).ToList();
                ViewBag.yoneticiList = yoneticiler;
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpGet]
        public ActionResult ProfileEdit(int id = 0)
        {
            var personel = new Calisanlar();
            personel.id = 0;
            Calisanlar model = new Calisanlar();
            if (id != 0)
            {
                personel = context.Calisanlar.FirstOrDefault(x => x.id == id);
                model = personel;
            }
            System.Diagnostics.Debug.WriteLine("aaa--" + model.id + "--");
            var departmanlar = context.Departmanlar.Select(x => new SelectListItem()
            {
                Text = x.adi,
                Value = x.id.ToString()
            }).ToList();
            ViewBag.departmanList = departmanlar;

            var yoneticiler = context.Calisanlar.Select(x => new SelectListItem()
            {
                Text = x.ad + " " + x.soyad,
                Value = x.id.ToString()
            }).Where(x => x.Value != model.id.ToString()).ToList();
            ViewBag.yoneticiList = yoneticiler;
          
            return View(model);
        }

        [HttpPost]
        public ActionResult ProfileEdit(Calisanlar model)
        {
            context.Entry<Calisanlar>(model).State = System.Data.Entity.EntityState.Modified;
            try
            {
                context.SaveChanges();
                TempData["resultInfo"] = "Bilgiler Güncellendi!";
                return RedirectToAction("Profile", "Account", new { id = model.id });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("aaa--" + "" + ex.Message);
                TempData["resultInfo"] ="Bilgiler Güncellenemedi!";
                var user = context.Calisanlar.FirstOrDefault(x => x.id == model.id);
                PersonelModel personel = new PersonelModel();
                personel.calisan = user;
                context.Entry<Calisanlar>(model).State = System.Data.Entity.EntityState.Added;
                return RedirectToAction("ProfileEdit", "Account", new { id = model.id });
            }
        }

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                var admin = context.Admin.FirstOrDefault(x => x.username == model.admin.username && x.password == model.admin.password);
                if (admin != null)
                {
                    Session["LogonAdmin"] = admin;
                    logonUserName = admin.username;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Admin Bilgileri Yanlış!";
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return View();


            }
        }

        public ActionResult Logout()
        {
            Session["LogonAdmin"] = null;
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Delete(int id)
        {
            var personel = context.Calisanlar.FirstOrDefault(x => x.id == id);
            if(context.Calisanlar.Any(x=>x.yoneticiId == personel.id))
            {
                TempData["resultInfo"] = "Bu personel yönetici olduğu için silinemez!";
                return RedirectToAction("Index", "Home");
            }
            context.Entry<Calisanlar>(personel).State = System.Data.Entity.EntityState.Deleted;
            try
            {
                context.SaveChanges();
                TempData["resultInfo"] = "Personel silindi!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["resultInfo"] = "Kullanıcı bir sebepten dolayı silinemedi!";
                return RedirectToAction("Index", "Home");
            }

        }
    }
}