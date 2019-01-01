using Datalager;
using System.Linq;
using System.Web.Mvc;
using webapp.Models;


namespace webapp.Controllers
{
    public class SearchFriendsController : Controller

    {
        [HttpPost]
        public ActionResult Search(FriendsViewModel model)
        {
            using (var db = new DejtDbContext())
            {
                var matchingProfiles = db.Profiles.Where(x => x.Favoritkaka == model.SearchText).ToList();

                var friendsviewModel = new FriendsViewModel();
                friendsviewModel.Profiles = matchingProfiles;
                friendsviewModel.SearchText = model.SearchText;

                return View("~/Views/Home/Vänner.cshtml", friendsviewModel);
            }

        }
    }
}
