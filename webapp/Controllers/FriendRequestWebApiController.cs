using Datalager;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace webapp.Controllers
{
    [RoutePrefix("api/friendrequest")]
    public class FriendRequestWebApiController : ApiController
    {
        [HttpGet]
        [Route("GetNumberOfFriendRequests")]
        public int GetNumberOfFriendRequests()
        {
            using (var db = new DejtDbContext())
            {
                var userId = User.Identity.GetUserId();
                if (userId == null)
                {
                    return 0;
                }
                var profile = db.Profiles.Single(x => x.UserId == userId);
                if (profile == null)
                {
                    return 0;
                }
                return profile.Mottagareförfrågan.Count(x => x.Accepted == false);
            }
        }
    }
}