using Datalager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class FriendsViewModel
    {
        public FriendsViewModel() {
            Profiles = new List<Profile>();
        }
        public string SearchText { get; set; }
        public List<Profile> Profiles { get; set; }
    }
}