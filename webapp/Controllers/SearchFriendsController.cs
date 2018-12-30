using Datalager;
using Datalager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;
using webapp.Helper;
using webapp.Models;


namespace webapp.Controllers
{
    public class SearchFriendsController : Controller

    {
        // GET: SearchFriends
        public ActionResult Friends(string searchName)
        //        {
        //            using (var db = new DejtDbContext())
        //            {

        //                var profile = db.Profiles;


        //                if (!string.IsNullOrEmpty(searchName))
        //                {
        //                    Profile = Profile.Where(p => p.Namn.Contains(searchName) || p.Name.Contains(searchName));
        //                }


        //                return View("Vänner".ToList());
        //            }
        //        }
        //    }
        //}
        {
            var db = new DejtDbContext();
            return View(db.Profiles.Where(p => p.Namn.Contains(searchName) || p.Namn.Contains(searchName)));


        } } }
