using Datalager;
using System.Web.Mvc;
using webapp.Models;

namespace webapp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Profil()
        {
            ViewBag.Message = " Din profil sida";
            using (var db = new DejtDbContext())
            {
                //hämta profil här
            }
            return View();

        }

        public ActionResult Vänner()
        {
            ViewBag.Message = "Här är dina vänner";

            return View();
        }



    }
}