using Datalager;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using webapp.Models;


namespace webapp.Controllers
{
    public class SearchFriendsController : Controller
    {
        //Söker efter profiler, matchar på favortikaka
        [HttpPost]
        public ActionResult Search(FriendsViewModel model)
        {
            using (var db = new DejtDbContext())
            {
                //Här matchar vi favoritkakan med det som skrevs i sökrutan 
                var matchingProfiles = db.Profiles.Where(x => x.Favoritkaka == model.SearchText).ToList();
                var friendsviewModel = new FriendsViewModel();
                friendsviewModel.Profiles = matchingProfiles;
                friendsviewModel.SearchText = model.SearchText;

                var userId = User.Identity.GetUserId();
                var profile = db.Profiles.SingleOrDefault(x => x.UserId == userId);

                //Läser upp dina kontaker från databasen
                //Vi läser både från avsändare och mottagare listorna eftersom det inte spelar någon roll 
                // vem som skickade vänförfrågan
                foreach (var x in profile.AvsändareFörfrågan.Where(x => x.Accepted))
                {
                    friendsviewModel.Kontakter.Add(x.Mottagare);
                }
                foreach (var x in profile.Mottagareförfrågan.Where(x => x.Accepted))
                {
                    friendsviewModel.Kontakter.Add(x.Avsändare);
                }

                //Om man inte får någon matchning 
                if (matchingProfiles.Count == 0 && model.SearchText != null)
                {
                    ViewBag.StatusMessage = "Finns ingen som har samma favoritkaka som dig :(";
                }

                return View("~/Views/Home/Vänner.cshtml", friendsviewModel);
            }
        }
    }
}

