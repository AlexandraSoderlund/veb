using Datalager;
using Datalager.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;
using webapp.Models;

namespace webapp.Controllers


{
    
    [RoutePrefix("api/profilepost")]
    public class ProfilePostController : ApiController
    {
        [HttpPost]
        [Route("savepost")]
        public void SavePost(PostViewModel viewModel)
        {
            using (var db = new DejtDbContext())
            {
                var userId = User.Identity.GetUserId();
                var mottagareProfile = db.Profiles.Single(x => x.Id == viewModel.MottagareId);
                var avsändareProfile = db.Profiles.Single(x => x.UserId == userId);

                var post = new Post();
                post.Avsändare = avsändareProfile;
                post.Mottagare = mottagareProfile;
                post.Text = viewModel.Text;

                mottagareProfile.MottagarePosts.Add(post);
                avsändareProfile.AvsändarePosts.Add(post);

                db.Posts.Add(post);

                db.SaveChanges();
            }
        }

    }
   
}