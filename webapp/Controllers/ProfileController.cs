using Datalager;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webapp.Models;

namespace webapp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public ActionResult SaveProfile(EditProfileViewModel model)
        {
            using (var db = new DejtDbContext())
            {
                var userId = User.Identity.GetUserId();
                var profile = db.Profiles.SingleOrDefault(x => x.UserId == userId);

                if (profile == null) {
                    profile = new Datalager.Models.Profile();
                    profile.UserId = userId;
                    db.Profiles.Add(profile);
                }

                profile.Description = model.Description;
                db.SaveChanges();
                ViewBag.StatusMessage = "Dina ändringar är sparade";
                
                return View("~/Views/Manage/Index.cshtml", model);
            }
        }
    }
}