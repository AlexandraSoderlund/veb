using System.Linq;
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

        public ActionResult Profil()
        {
            ViewBag.Message = " Din profil sida";
            using (var db = new DejtDbContext())
            {
                var förstaprofilen = db.Profiles.First();
                return View(förstaprofilen);
            }
            //return View();

        }

        public ActionResult Vänner()
        {
            ViewBag.Message = "Här är dina vänner";

            return View();
        }
        


    }
}