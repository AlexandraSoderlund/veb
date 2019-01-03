using Datalager;
using Datalager.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webapp.Models;

namespace webapp.Controllers
{
    public class FriendsRequestController : Controller
    {
        //hämtar en ProfileViewmodel och sätter fälten i FriendsRequest modelen 
        //till den datan som ProfileViewModel för den som klickar på skicka vänförfrågan..
        public ActionResult SendRequest(ProfileViewModel model)
        { using (var db = new DejtDbContext())
            {
                var userId = User.Identity.GetUserId();
                var mottagare = db.Profiles.Single(x => x.Id == model.Mottagare);
                var avsändare = db.Profiles.Single(x => x.UserId == userId);

                var Request = new FriendsRequest();
                var view = new ProfileViewModel();
                Request.Avsändare = avsändare;
                Request.Mottagare = mottagare;
                
                mottagare.Mottagareförfrågan.Add(Request);
                avsändare.AvsändareFörfrågan.Add(Request);

                db.Förfrågan.Add(Request);

                db.SaveChanges();



            }



                return View();
        }
    }
}