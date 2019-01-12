using Datalager;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace webapp.Controllers
{
    [RoutePrefix("api/friendrequest")]
    public class FriendRequestWebApiController : ApiController
    {

        //hämtar antal vänförfrågningar
        [HttpGet]
        [Route("GetNumberOfFriendRequests")]
        public int GetNumberOfFriendRequests()
        {
            using (var db = new DejtDbContext())
            {
                var userId = User.Identity.GetUserId();

                //vi kollar om användaren är inloggad om inte skickar vi tillbaka 0
                if (userId == null)
                {
                    return 0;
                }
                //hämtar upp profilen för den inloggade, om profilen inte finns returnar vi 0
                var profile = db.Profiles.SingleOrDefault(x => x.UserId == userId);
                if (profile == null)
                {
                    return 0;
                }

                //Returnar antalet vänförfrågningar som inte är accepterade än
                return profile.Mottagareförfrågan.Count(x => x.Accepted == false);
            }
        }
    }
}