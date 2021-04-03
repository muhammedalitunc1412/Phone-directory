using System.Web.Mvc;
using TelefonRehberi.Entity;

namespace Telefon_Rehberi_MVC.Controllers
{
    public class BaseController : Controller
    {
        protected TelefonRehberiEntities context { get; private set; }
        public static string logonUserName {get; set;}
        public BaseController()
        {
            context = new TelefonRehberiEntities();
        }
    }
}