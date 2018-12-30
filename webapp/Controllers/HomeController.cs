using Datalager;
using Datalager.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using webapp.Models;

namespace webapp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new DejtDbContext())
            {
                var v = new IndexExempelAnvändareViewModel();
                v.Profiles = db.Profiles.ToList();

                return View(v);
            }
        }

        [Authorize]
        public ActionResult Profil()
        {
            ViewBag.Message = " Din profil sida";
            using (var db = new DejtDbContext())
            {
                var userId = User.Identity.GetUserId();
                var profile = db.Profiles.SingleOrDefault(x => x.UserId == userId);
                return View(profile);
            }
   

        }

        public ActionResult Vänner()
        {
            ViewBag.Message = "Här är dina vänner";
            using(var db = new DejtDbContext())

            return View();
        }



    }
}