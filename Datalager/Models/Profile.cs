﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Datalager.Models
{
    public class Profile
    {
        public Profile()
        {
            MottagarePosts = new List<Post>();
            AvsändarePosts = new List<Post>();
        }

        [Key]
        public int Id { get; set; }

        public string ProfileImageUrl { get; set; }
        public string Description { get; set; }
        public string Favoritkaka { get; set; }
        public string UserId { get; set; }
        public string Namn { get; set; }

        public virtual List<Post> MottagarePosts { get; set; }
        public virtual List<Post> AvsändarePosts { get; set; }

    }
}