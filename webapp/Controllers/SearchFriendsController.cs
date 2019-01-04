using Datalager;
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


                //if (!ModelState.IsValid)
                //{
                //    var values = ModelState.Values;
                //    var packages = _context.Packages.ToList();
                //    viewModel.Packages = packages;

                    //if (!matchingProfiles.HasValue)

                    //{ ViewBag.StatusMessage = "Finns ingen som heter så"; }


                    return View("~/Views/Home/Vänner.cshtml", friendsviewModel);
            
                

            }





        }    

}


}

