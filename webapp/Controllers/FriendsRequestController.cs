﻿using Datalager;
using Datalager.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webapp.Helper;
using webapp.Models;

namespace webapp.Controllers
{
    public class FriendsRequestController : Controller
    {
        public ActionResult FriendRequests()
        {
            using (var db = new DejtDbContext())
            {
                var userId = User.Identity.GetUserId();
                var profile = db.Profiles.Single(x => x.UserId == userId);
                var profileViewModel = ProfileHelper.GetProfileViewModel(profile.Id, userId);
                return View(profileViewModel);
            }
        }
        
        //Skickar en vänförfrågan
        public ActionResult SendRequest(ProfileViewModel model)
        {
            using (var db = new DejtDbContext())
            {
                var userId = User.Identity.GetUserId();
                var mottagare = db.Profiles.Single(x => x.Id == model.Id);
                var avsändare = db.Profiles.Single(x => x.UserId == userId);

                var Request = new FriendsRequest();
                Request.Avsändare = avsändare;
                Request.Mottagare = mottagare;
                Request.Accepted = false;

                mottagare.Mottagareförfrågan.Add(Request);
                avsändare.AvsändareFörfrågan.Add(Request);


                db.Förfrågan.Add(Request);

                //Får textmeddelande när man försöker lägga till sig själv som vän, förfrågan tas bort från databasen
                if (Request.Avsändare.Equals(mottagare))
                {
                    db.Förfrågan.Remove(Request);
                    ViewBag.StatusMessage = "du kan inte lägga till dig själv";
                }

                db.SaveChanges();

                //hämtar den uppdaterade profilen på den man är inne på
                var uppdateradProfil = ProfileHelper.GetProfileViewModel(mottagare.Id, userId);

                return View("~/Views/Home/Profil.cshtml", uppdateradProfil);


            }

        }

        [HttpGet]
        public ActionResult AcceptFriendRequest(int friendRequestId)
        {
            using (var db = new DejtDbContext())
            {
                //Accepterar förfrågan
                var friendRequest = db.Förfrågan.Single(x => x.Id == friendRequestId);
                friendRequest.Accepted = true;
                db.SaveChanges();

                //Laddar om sidan
                var userId = User.Identity.GetUserId();
                var profile = db.Profiles.Single(x => x.UserId == userId);
                var friendsView = FriendsHelper.GetViewModel(profile.Id);
                return View("~/Views/Home/Vänner.cshtml", friendsView);
            }
        }

        [HttpGet]
        public ActionResult DeclineFriendRequest(int friendRequestId)
        {
            using (var db = new DejtDbContext())
            {
                //tar bort förfrågan
                var friendRequest = db.Förfrågan.Single(x => x.Id == friendRequestId);
                db.Förfrågan.Remove(friendRequest);
                db.SaveChanges();

                //Laddar om sidan
                var userId = User.Identity.GetUserId();
                var profile = db.Profiles.Single(x => x.UserId == userId);
                var friendsView = FriendsHelper.GetViewModel(profile.Id);
                return View("~/Views/Home/Vänner.cshtml", friendsView);

            }
        }
    }
}