using Datalager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webapp.Models;

namespace webapp.Helper
{
    public class ProfileHelper
    {
        public static ProfileViewModel GetProfileViewModel(int id)
        {
            using (var db = new DejtDbContext())
            {
                var profile = db.Profiles.Single(x => x.Id == id);

                var profileViewModel = new ProfileViewModel();
                profileViewModel.Id = profile.Id;
                profileViewModel.MottagarePosts = profile.MottagarePosts.Select(x => new PostViewModel
                {
                    AvsändareNamn = x.Avsändare.Namn,
                    Text = x.Text,
                }).ToList();
                profileViewModel.Namn = profile.Namn;
                profileViewModel.Description = profile.Description;
                profileViewModel.Favoritkaka = profile.Favoritkaka;
                profileViewModel.ProfileImageUrl = profile.ProfileImageUrl;
                return profileViewModel;
            }

        }
    }
}