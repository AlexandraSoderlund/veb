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
        //Sparar ändringar i en profil
        [HttpPost]
        public ActionResult SaveProfile(EditProfileViewModel model)
        {
            using (var db = new DejtDbContext())
            {

                var userId = User.Identity.GetUserId();
                var profile = db.Profiles.SingleOrDefault(x => x.UserId == userId);

                //Om profil inte finns så skapar vi den
                if (profile == null)
                {
                    profile = new Datalager.Models.Profile();
                    profile.UserId = userId;
                    db.Profiles.Add(profile);
                }

                //Ändrar profilbild om man valt en ny
                if (model.ProfileImage != null)
                {
                    profile.ProfileImageUrl = "~/profilbilder/" + model.ProfileImage.FileName;

                    SaveProfileImage(model);
                }


                model.ProfileImageUrl = profile.ProfileImageUrl;

                //Om alla fält är giltiga så sparas det
                if (ModelState.IsValid)
                {
                    profile.Description = model.Description;
                    profile.Namn = model.Namn;
                    profile.Favoritkaka = model.Favoritkaka;

                    db.SaveChanges();
                    ViewBag.StatusMessage = "Dina ändringar är sparade";
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


