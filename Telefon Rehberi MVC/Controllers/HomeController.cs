using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TelefonRehberi.Dto.Departman;
using TelefonRehberi.Entity;

namespace Telefon_Rehberi_MVC.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            //NT AUTHORITY\NetworkService
            //DESKTOP-4J8C4OH
            List<Calisanlar> calisanlar = context.Calisanlar.Where(x => x.id != 0).ToList();
            return View(calisanlar);
        }

        [HttpGet]
        public ActionResult DepartmentControl()
        {
            List<DepartmanListModel> departmanListModel = new List<DepartmanListModel>();
            List<Departmanlar> departmanList = context.Departmanlar.Where(x => x.id != 0).ToList();
            System.Diagnostics.Debug.WriteLine("aa--" + departmanList.Count + "--");
            List<int> personelSayisi = new List<int>();
            for (int i = 0; i < departmanList.Count; i++)
            {
                DepartmanListModel model = new DepartmanListModel
                {
                    departman = departmanList[i]
                };
                System.Diagnostics.Debug.WriteLine("aa--" + model.departman + "--");
                System.Diagnostics.Debug.WriteLine("aa--" + departmanList[i] + "--");
                System.Diagnostics.Debug.WriteLine("aa--" + i + "--");
                model.PersonelSayisi = Convert.ToInt32(context.Calisanlar.Where(x => x.departmanId == model.departman.id).ToList().Count());
                departmanListModel.Add(model);
            }
            return View(departmanListModel);
        }

        public ActionResult Delete(int id=0)
        {
            if (id == 0)
            {
                TempData["resultInfo"] = "Departman içinde mevcut personel olduğu için silinemedi.";
                return RedirectToAction("DepartmentControl", "Home");
            }
            var department = context.Departmanlar.FirstOrDefault(x => x.id == id);
            string name = department.adi;
            context.Entry<Departmanlar>(department).State = System.Data.Entity.EntityState.Deleted;
            try
            {
                context.SaveChanges();
                TempData["resultInfo"] = name + " departmanı silindi.";
                return RedirectToAction("DepartmentControl", "Home");
            }
            catch (Exception ex)
            {
                TempData["resultInfo"] = "Silme İşlemi Gerçekleştirilemedi.";
                return RedirectToAction("DepartmentControl", "Home");
            }

        }


        [HttpGet]
        public ActionResult DepartmanCreate()
        {
            if (Session["LogonAdmin"] != null)
            {
                Departmanlar model = new Departmanlar();

                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult DepartmanCreate(Departmanlar model)
        {
           
            System.Diagnostics.Debug.WriteLine("aa--" + model.adi + "--");
            if (Session["LogonAdmin"] == null)
            {
                TempData["resultInfo"] = "Oturum süreniz dolmuştur. Lütfen Oturum Açıp Tekrar Deneyiniz!";
                return RedirectToAction("Login", "Action");
            }
            
            if(model.adi != "")
            {
                if(context.Departmanlar.Any(x=>x.adi == model.adi))
                {
                    TempData["resultInfo"] = "Aynı isimde bölüm var!";
                    return View();
                }
                try
                {

                    context.Entry<Departmanlar>(model).State = System.Data.Entity.EntityState.Added;
                    TempData["resultInfo"] = model.adi + " departmanı eklendi.";
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    TempData["resultInfo"] = ex.Message;
                }
                return RedirectToAction("DepartmentControl", "Home");
            }
            else
            {
                TempData["resultInfo"] = "Departman Adı Giriniz!";
                return RedirectToAction("DepartmentControl", "Home");
            }

        }

    }



}
/*
     DepartmanListModel model = new DepartmanListModel();
            List<Departmanlar> departmanList = context.Departmanlar.Where(x => x.id != 0).ToList();
            List<int> personelSayisi = new List<int>();
            for (int i = 0; i < departmanList.Count; i++)
                personelSayisi.Add(context.Calisanlar.Where(x => x.departmanID == departmanList[i].id).Count());
            model.departman = departmanList;
            model.PersonelSayisi = personelSayisi;
            return View(model);
     */
