using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace veb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profil()
        {
            ViewBag.Message = " Din profil sida";

            return View();
        }

        public ActionResult Vänner()
        {
            ViewBag.Message = "Här är dina vänner";

            return View();
        }
        


    }
}