using Datalager;
using Datalager.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using webapp.Helper;
using webapp.Models;

namespace webapp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        //Gör ändringar samt sparar ändringar som görs Manage Index vyn samt returnerar samma vy när man trycker på spara
        [HttpPost]
        public ActionResult SaveProfile(EditProfileViewModel model)
        {
            using (var db = new DejtDbContext())
            {

                var userId = User.Identity.GetUserId();
                var profile = db.Profiles.SingleOrDefault(x => x.UserId == userId);

                if (profile == null)
                {
                    profile = new Datalager.Models.Profile();
                    profile.UserId = userId;
                    db.Profiles.Add(profile);
                }

                if (model.ProfileImage != null)
                {
                    profile.ProfileImageUrl = "~/profilbilder/" + model.ProfileImage.FileName;

                    SaveProfileImage(model);
                }


                model.ProfileImageUrl = profile.ProfileImageUrl;
                try
                {
                    if (ModelState.IsValid)
                    {
                        profile.Description = model.Description;
                        profile.Namn = model.Namn;
                        profile.Favoritkaka = model.Favoritkaka;

                        db.SaveChanges();
                        ViewBag.StatusMessage = "Dina ändringar är sparade";
                    }
                }
                //Catchen fungerar inte, vet inte varför, ska man ha tryen över hela? 
                //    Ändringarna ska inte sparas i databasen om valideringen är fel.
                catch {
                    throw new Exception("Kunde inte spara ändringar, försök igen");
                }


                return View("~/Views/Manage/Index.cshtml", model);
            }
        }

        //Sparar en profilbild och kontrollerar om mappen med bilder finns

        public void SaveProfileImage(EditProfileViewModel model)
        {
            var imageFolder = Server.MapPath("~/profilbilder");

            if (!Directory.Exists(imageFolder))
            {
                Directory.CreateDirectory(imageFolder);
            }
            model.ProfileImage.SaveAs(imageFolder + "/" + model.ProfileImage.FileName);

        }
    }
};


