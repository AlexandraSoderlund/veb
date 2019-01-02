﻿using Datalager.Models;
using System.Collections.Generic;

namespace webapp.Models
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Description { get; set; }
        public string Favoritkaka { get; set; }
        public string Namn { get; set; }

        public string NyPostText { get; set; }

        public virtual List<PostViewModel> MottagarePosts { get; set; }
        public bool Accepted { get; set; }
        public string Avsändare { get; set; }
        public int Mottagare { get; set; }
    }
}