using Datalager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class FriendsViewModel
    {
        public FriendsViewModel()
        {
            Profiles = new List<Profile>();
        }
        [Required(ErrorMessage ="Du måste fylla i en favoritkaka")]
        [StringLength(40,ErrorMessage ="Skriv minst 3-40 bokstäver", MinimumLength = 3)]
        [RegularExpression(@"^([a-åäöA-ÅÄÖ \.\&\'\-]+)$", ErrorMessage = "Använd endast bokstäver")]
        public string SearchText { get; set; }
        public List<Profile> Profiles { get; set; }
    }
}