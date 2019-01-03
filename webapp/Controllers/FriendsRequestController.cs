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
        // GET: FriendsRequest
        public ActionResult SendRequest(FriendsRequestViewModel model)
        { using (var db = new DejtDbContext())
            {
                var userId = User.Identity.GetUserId();
                var mottagare = db.Profiles.Single(x => x.Id == model.Mottagare);
                var avsändare = db.Profiles.Single(x => x.UserId == userId);

                var Request = new FriendsRequest();
                Request.Avsändare = avsändare;
                Request.Mottagare = mottagare;
                Request.Accepted = false;
                
                mottagare.Mottagareförfrågan.Add(Request);
                avsändare.AvsändareFörfrågan.Add(Request);

                db.Förfrågan.Add(Request);

                db.SaveChanges();

                return View("~/Views/Home/Profil.cshtml", model);


            }

        }
    }
}