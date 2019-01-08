using Datalager;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using webapp.Helper;
using webapp.Models;

namespace webapp.Controllers
{
    public class HomeController : Controller
    {
        //Returnerar en IndexView som även visar en lista på Exempelanvändare
        public ActionResult Index()
        {
            using (var db = new DejtDbContext())
            {
                var v = new IndexExempelAnvändareViewModel();
                v.Profiles = db.Profiles.ToList();

                return View(v);
            }
        }

        //Returnerar en view med min profil
        [Authorize]
        public ActionResult MinProfil()
        {
            using (var db = new DejtDbContext())
            {
                var userId = User.Identity.GetUserId();
                var profile = db.Profiles.SingleOrDefault(x => x.UserId == userId);
                var profileViewModel = ProfileHelper.GetProfileViewModel(profile.Id);
                return View(profileViewModel);
            }
        }

        //Tar emot ett profilId och returnerar en profileViewModel med en profil på en annan användare??
        [Authorize]
        public ActionResult Profil(int profileId)
        {
            using (var db = new DejtDbContext())
            {
                var profile = db.Profiles.SingleOrDefault(x => x.Id == profileId);
                var profileViewModel = ProfileHelper.GetProfileViewModel(profile.Id);
                return View(profileViewModel);
            }
        }
             
        [Authorize]
        public ActionResult Vänner()
        {
            ViewBag.Message = "Här är dina vänner";
            using (var db = new DejtDbContext())
            {
                var userId = User.Identity.GetUserId();
                var profile = db.Profiles.SingleOrDefault(x => x.UserId == userId);
                var friendsView = FriendsHelper.GetViewModel(profile.Id);
                return View(friendsView);
            }

        }
    }
}