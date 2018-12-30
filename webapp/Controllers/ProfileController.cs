using Datalager;
using Datalager.Models;
using Microsoft.AspNet.Identity;
using System;
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
        [HttpPost]
        public ActionResult SavePost(ProfileViewModel viewModel)
        {
            using (var db = new DejtDbContext())
            {
                var userId = User.Identity.GetUserId();
                var mottagareProfile = db.Profiles.Single(x => x.Id == viewModel.Id);
                var avsändareProfile = db.Profiles.Single(x => x.UserId == userId);

                var post = new Post();
                post.Avsändare = avsändareProfile;
                post.Mottagare = mottagareProfile;
                post.Text = viewModel.NyPostText;

                mottagareProfile.MottagarePosts.Add(post);
                avsändareProfile.AvsändarePosts.Add(post);

                db.Posts.Add(post);

                db.SaveChanges();

                var updatedViewModel = ProfileHelper.GetProfileViewModel(mottagareProfile.Id);

                return View("~/Views/Home/Profil.cshtml", updatedViewModel);
            }
        }

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

                profile.Description = model.Description;
                profile.Namn = model.Namn;
                profile.Favoritkaka = model.Favoritkaka;

                if (model.ProfileImage != null)
                {
                    profile.ProfileImageUrl = "~/profilbilder/" + model.ProfileImage.FileName;

                    SaveProfileImage(model);
                }

                db.SaveChanges();
                ViewBag.StatusMessage = "Dina ändringar är sparade";
                model.ProfileImageUrl = profile.ProfileImageUrl;



                return View("~/Views/Manage/Index.cshtml", model);
            }
        }

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


