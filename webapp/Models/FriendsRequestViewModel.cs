using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class FriendsRequestViewModel
    {
        public int Id { get; set; }
        public bool Accepted { get; set; }
        public string Avsändare { get; set; }
        public int Mottagare { get; set; }
    }
}
