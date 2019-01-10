using Datalager;
using Datalager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webapp.Models;

namespace webapp.Helper
{
    public class ProfileHelper
    {
        //Hämtar en profil från databasen och mappar det till en viewmodel
        public static ProfileViewModel GetProfileViewModel(int profilId, string inloggadUserId)
        {
            using (var db = new DejtDbContext())
            {
                var inloggadProfile = db.Profiles.Single(x => x.UserId == inloggadUserId);
                var profile = db.Profiles.Single(x => x.Id == profilId);

                var profileViewModel = new ProfileViewModel();
                profileViewModel.Id = profile.Id;

                profileViewModel.MottagarePosts = GetPostViewModels(profile.MottagarePosts);

                profileViewModel.FriendRequests = profile.Mottagareförfrågan
                    .OrderByDescending(x => x.Id)
                    .Select(x => new FriendsRequestViewModel
                    {
                        Id = x.Id,
                        AvsändareProfile = x.Avsändare,
                        Accepted = x.Accepted,
                    })
                    .ToList();

                profileViewModel.Namn = profile.Namn;
                profileViewModel.Description = profile.Description;
                profileViewModel.Favoritkaka = profile.Favoritkaka;
                profileViewModel.ProfileImageUrl = profile.ProfileImageUrl;
                
                profileViewModel.HarSkickatFörfrågan = profile.Mottagareförfrågan.Any(x => x.Avsändare.Id == inloggadProfile.Id);
                profileViewModel.ÄrVänner = profile.Mottagareförfrågan
                    .Any(x => x.Avsändare.Id == inloggadProfile.Id && x.Accepted)
                    ||
                    profile.AvsändareFörfrågan
                    .Any(x => x.Mottagare.Id == inloggadProfile.Id && x.Accepted);
                // Neeed helps
                profileViewModel.ÄrSigSjälv = profile.AvsändareFörfrågan.Any(x => x.Id == inloggadProfile.Id);

                return profileViewModel;
            }
        }

        private static List<PostViewModel> GetPostViewModels(List<Post> posts)
        {
            return posts
                .OrderByDescending(x => x.Id)
                .Select(x => new PostViewModel
                {
                    AvsändareNamn = x.Avsändare.Namn,
                    Text = x.Text,
                })
                .ToList();
        }
    }
}