using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TelefonRehberi.Entity;

namespace TelefonRehberi.Dto.Account
{
    public class PersonelModel
    {
        public Calisanlar calisan { get; set; }
        public string adi;
        public string yoneticiAdSoyad;
        public List<SelectListItem> departmanList { get; set; }
        public List<SelectListItem> yoneticiList { get; set; }
        public int yoneticiID;
        public int departmanID;
    }
}