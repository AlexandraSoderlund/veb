using Datalager;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using webapp.Models;


namespace webapp.Controllers
{
    public class SearchFriendsController : Controller

    {
        //Tar in ett obejkt av FriendsViewModel och kollar om den Favoritkaka man skriver in matchar Favoritkakan i modelen
        [HttpPost]
        public ActionResult Search(FriendsViewModel model)
        {
            using (var db = new DejtDbContext())

            {
                var matchingProfiles = db.Profiles.Where(x => x.Favoritkaka == model.SearchText).ToList();
                var friendsviewModel = new FriendsViewModel();
                friendsviewModel.Profiles = matchingProfiles;
                friendsviewModel.SearchText = model.SearchText;

                var userId = User.Identity.GetUserId();
                var profile = db.Profiles.SingleOrDefault(x => x.UserId == userId);

                foreach (var x in profile.AvsändareFörfrågan.Where(x => x.Accepted))
                {
                    friendsviewModel.Kontakter.Add(x.Mottagare);
                }
                foreach (var x in profile.Mottagareförfrågan.Where(x => x.Accepted))
                {
                    friendsviewModel.Kontakter.Add(x.Avsändare);
                }


                if (matchingProfiles.Count == 0 && model.SearchText!=null)  

                {ViewBag.StatusMessage = "Finns ingen som har samma favoritkaka som dig :("; }


                return View("~/Views/Home/Vänner.cshtml", friendsviewModel);
            
                

            }





        }    

}


}

