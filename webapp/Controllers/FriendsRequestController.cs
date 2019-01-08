using Datalager;
using Datalager.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webapp.Helper;
using webapp.Models;

namespace webapp.Controllers
{
    public class FriendsRequestController : Controller
    {
        // GET: FriendsRequest
        //hämtar en ProfileViewmodel och sätter fälten i FriendsRequest modelen 
        //till den datan som ProfileViewModel för den som klickar på skicka vänförfrågan..

        public ActionResult SendRequest(ProfileViewModel model)
        {
            using (var db = new DejtDbContext())
            {
                var userId = User.Identity.GetUserId();
                var mottagare = db.Profiles.Single(x => x.Id == model.Id);
                var avsändare = db.Profiles.Single(x => x.UserId == userId);

                var Request = new FriendsRequest();
                Request.Avsändare = avsändare;
                Request.Mottagare = mottagare;
                Request.Accepted = false;

                mottagare.Mottagareförfrågan.Add(Request);
                avsändare.AvsändareFörfrågan.Add(Request);

                db.Förfrågan.Add(Request);

                db.SaveChanges();
                var uppdateradProfil = ProfileHelper.GetProfileViewModel(mottagare.Id);

                return View("~/Views/Home/Profil.cshtml", uppdateradProfil);


            }

        }

        [HttpGet]
        public ActionResult AcceptFriendRequest(int friendRequestId)
        {
            using (var db = new DejtDbContext())
            {
                var friendRequest = db.Förfrågan.SingleOrDefault(x => x.Id == friendRequestId);
                var uppdateradProfil = ProfileHelper.GetProfileViewModel(friendRequest.Mottagare.Id);

                return View("~/Views/Home/Profil.cshtml", uppdateradProfil);

            }
        }
    }
}