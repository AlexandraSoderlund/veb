using Datalager;
using System.Linq;
using webapp.Models;

namespace webapp.Helper
{
    public class FriendsHelper
    {
        //tar emot ett profilID som stämmer överens med profilidet i databasen.När en vänförfrågan är accepterad läggs
    //    den nya vännen till i kontaktlistan. 
        public static FriendsViewModel GetViewModel(int profileId)
        {
            using (var db = new DejtDbContext())
            {
                var friendsView = new FriendsViewModel();
                var profile = db.Profiles.SingleOrDefault(x => x.Id == profileId);

                foreach (var x in profile.AvsändareFörfrågan.Where(x => x.Accepted))
                {
                    friendsView.Kontakter.Add(x.Mottagare);
                }
                foreach (var x in profile.Mottagareförfrågan.Where(x => x.Accepted))
                {
                    friendsView.Kontakter.Add(x.Avsändare);
                }

                return friendsView;
            }

        }
    }
}