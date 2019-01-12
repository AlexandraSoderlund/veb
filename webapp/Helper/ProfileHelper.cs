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
        /// <summary>
        /// Hämtar en profil från databasen och mappar det till en viewmodel
        /// </summary>
        /// <param name="profilId">Id för den profilen vi ska hämta</param>
        /// <param name="inloggadUserId">Userid för den inloggade användaren, 
        /// vi använder den för att kolla vilken relationen den innloggade användaren har med profilen
        /// den besöker</param>
        public static ProfileViewModel GetProfileViewModel(int profilId, string inloggadUserId)
        {
            using (var db = new DejtDbContext())
            {
                //Hämtar profilen för den vi är inne på och den vi är inloggad som 
                var inloggadProfile = db.Profiles.Single(x => x.UserId == inloggadUserId);
                var profile = db.Profiles.Single(x => x.Id == profilId);

                var profileViewModel = new ProfileViewModel();
                profileViewModel.Id = profile.Id;

                //Hämtar alla posts som profilen vi är inne på har mottagit
                profileViewModel.MottagarePosts = GetPostViewModels(profile.MottagarePosts);

                //Hämtar alla vänförfrågningar som profilen vi är inne på har mottagit
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
                
                //Kollar vilken typ av relation den inloggade användaren har till profilen
                profileViewModel.HarSkickatFörfrågan = profile.Mottagareförfrågan.Any(x => x.Avsändare.Id == inloggadProfile.Id);
                profileViewModel.ÄrVänner = profile.Mottagareförfrågan
                    .Any(x => x.Avsändare.Id == inloggadProfile.Id && x.Accepted)
                    ||
                    profile.AvsändareFörfrågan
                    .Any(x => x.Mottagare.Id == inloggadProfile.Id && x.Accepted);

                return profileViewModel;
            }
        }

        //Mappar posts till postviewmodels
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