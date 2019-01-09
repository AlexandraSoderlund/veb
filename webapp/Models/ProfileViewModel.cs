using Datalager.Models;
using System.Collections.Generic;

namespace webapp.Models
{
    public class ProfileViewModel
    {
        public ProfileViewModel()
        {
            FriendRequests = new List<FriendsRequestViewModel>();
            MottagarePosts = new List<PostViewModel>();
        }
        public int Id { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Description { get; set; }
        public string Favoritkaka { get; set; }
        public string Namn { get; set; }
        public string NyPostText { get; set; }
        public List<PostViewModel> MottagarePosts { get; set; }
        public List<FriendsRequestViewModel> FriendRequests { get; set; }
        public bool HarSkickatFörfrågan { get; set; }
        public bool ÄrVänner { get; set; }
    }
}