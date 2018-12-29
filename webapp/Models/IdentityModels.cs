using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Datalager;
using Datalager.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace webapp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class DejtDatabaseInitializer : DropCreateDatabaseAlways<DejtDbContext>
    {
        protected override void Seed(DejtDbContext context)
        {
            SeedUsers(context);
        }

        public void SeedUsers(DejtDbContext dejtDbContext)
        {
            using (var userDatabaseContext = new ApplicationDbContext())
            {
                var users = userDatabaseContext.Users.ToList();

                foreach (var u in users)
                {
                    var profile = new Profile();
                    profile.UserId = u.Id;
                    profile.Namn = GetRandomName();
                    profile.Description = GetRandomBeskrivning();
                    profile.Favoritkaka = GetRandomKaka();
                    profile.ProfileImageUrl = "~/bilder/" + GetRandomImageUrl();
                    dejtDbContext.Profiles.Add(profile);
                }
            }

            dejtDbContext.SaveChanges();
        }

        private string GetRandomName()
        {
            var names = new List<string>
            {
                "Arne Kanelbulle",
                "Stina Bulleälskaren",
                "Helena Helgräddarn",
                "Anna Rulltårta",
                "Henrik Hörnkaka",
                "Torsten Tårtspade",
                "Ludvig Lussebulle"
            };

            return GetRandomFromList(names);
        }

        private string GetRandomKaka()
        {
            var kakor = new List<string>
            {
                "Bulle",
                "Tårta",
                "Sockerkaka",
                "Kladdkaka",
                "Vaniljbulle",
                "Lussebulle",
                "Hallongrotta",
                "Snickerskaka"
            };

            return GetRandomFromList(kakor);
        }

        private string GetRandomBeskrivning()
        {
            var beskrivningar = new List<string>
            {
                "Glad kärleksmumsare på 44 jordsnurr",
                "Gillar framförallt vanilj i solnedgången",
                "Behöver kärleksmums",
                "Vill dela en kanelbulle med någon jag älskar",
                "Fika is life",
                "En slät kopp kaffe duger inte för mig",
                "Kanelbulle and chill"
            };

            return GetRandomFromList(beskrivningar);
        }

        private string GetRandomImageUrl()
        {
            var beskrivningar = new List<string>
            {
                "balloon-991680_1920.jpg",
                "girl-1722402_1920.jpg",
                "love-3061483_1920.jpg",
                "man-1150058_1920.jpg",
                "man-1245658_1920.jpg",
                "photographer-407068_1920.jpg",
                "running-573762_1920.jpg",
            };

            return GetRandomFromList(beskrivningar);
        }


        private string GetRandomFromList(List<string> listOfStrings)
        {
            var rand = new Random();
            var next = rand.Next(0, listOfStrings.Count - 1);
            return listOfStrings[next];
        }
    }
}