using System.Web;

namespace webapp.Models
{
    public class EditProfileViewModel
    {
        public string Description { get; set; }
        public HttpPostedFileBase ProfileImage { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Favoritkaka { get; set; }
        public string Namn { get; set; }
        //public string LoggText { get; set; }

    }
}