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
    // Här är klassen som skapar vår databas och fyller den med testdata så att den inte är tom
    // Databasen tas bort innan den skapas på nytt det gör vi får att databaschemat ska stämma om 
    // vi gjort ändringar i vår modell.
    public class DejtDatabaseInitializer : DropCreateDatabaseAlways<DejtDbContext>
    {
        private Random rand = new Random();

        //Den här metoden körs när databasen är skapad
        // då fyller vi den med testdata.
        protected override void Seed(DejtDbContext context)
        {
            SeedUsers();
            SeedProfiles(context);
        }

        public void SeedUsers()
        {
            using (var context = new ApplicationDbContext())
            {
                var users = new List<ApplicationUser>
                {
                    new ApplicationUser{ Email = "test1@karleksmums.se" },
                    new ApplicationUser{ Email = "test2@karleksmums.se" },
                    new ApplicationUser{ Email = "test3@karleksmums.se" },
                    new ApplicationUser{ Email = "test4@karleksmums.se" },
                    new ApplicationUser{ Email = "test5@karleksmums.se" },
                };

                var hasTestanvändareSkapats = context.Users.Any(dbUser => dbUser.Email == "test1@karleksmums.se");

                if (hasTestanvändareSkapats == false)
                {
                    var store = new UserStore<ApplicationUser>(context);
                    var manager = new UserManager<ApplicationUser>(store);

                    foreach (var u in users)
                    {
                        u.UserName = u.Email;
                        var result = manager.Create(u, "Test1!");
                    }
                }
            }
        }

        public void SeedProfiles(DejtDbContext dejtDbContext)
        {
            using (var userDatabaseContext = new ApplicationDbContext())
            {
                var users = userDatabaseContext.Users.ToList();

                foreach (var u in users)
                {
                    var profile = new Profile();
                    profile.UserId = u.Id;

                    if (u.Email == "test1@karleksmums.se")
                    {
                        profile.Namn = "Arne Kanelbulle";
                        profile.Description = "Glad kärleksmumsare på 44 jordsnurr";
                        profile.Favoritkaka = "Bulle";
                        profile.ProfileImageUrl = "~/bilder/man-1150058_1920.jpg";
                    }
                    else if (u.Email == "test2@karleksmums.se")
                    {
                        profile.Namn = "Anna Rulltårta";
                        profile.Description = "Fika is life";
                        profile.Favoritkaka = "Tårta";
                        profile.ProfileImageUrl = "~/bilder/girl-1722402_1920.jpg";
                    }
                    else if (u.Email == "test3@karleksmums.se")
                    {
                        profile.Namn = "Torsten Tårtspade";
                        profile.Description = "En slät kopp kaffe duger inte för mig";
                        profile.Favoritkaka = "Tårta";
                        profile.ProfileImageUrl = "~/bilder/man-1245658_1920.jpg";
                    } 
                    else if (u.Email == "test4@karleksmums.se")
                    {
                        profile.Namn = "Helena Helgräddarn";
                        profile.Description = "Behöver kärleksmums";
                        profile.Favoritkaka = "Snickerskaka";
                        profile.ProfileImageUrl = "~/bilder/running-573762_1920.jpg";
                    }
                    else if (u.Email == "test5@karleksmums.se")
                    {
                        profile.Namn = "Henrik Hörnkaka";
                        profile.Description = "Kanelbulle and chill";
                        profile.Favoritkaka = "Kladdkaka";
                        profile.ProfileImageUrl = "~/bilder/balloon-991680_1920.jpg";
                    }
                    else
                    {
                        profile.Namn = GetRandomName();
                        profile.Description = GetRandomBeskrivning();
                        profile.Favoritkaka = GetRandomKaka();
                        profile.ProfileImageUrl = "~/bilder/" + GetRandomImageUrl();
                    }
                   
                    dejtDbContext.Profiles.Add(profile);
                }
            }

            dejtDbContext.SaveChanges();
        }

        private string GetRandomName()
        {
            var names = new List<string>
            {
                "Stina Bulleälskaren",
                "Ludvig Lussebulle",
                "Sanna Semla",
                "Charlie Chokladboll",
                "Daniel Dammsugare",
                "Marie Mandelkubb",
                "Jens Jordgubbssnitt"
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
            var next = rand.Next(0, listOfStrings.Count - 1);
            return listOfStrings[next];
        }
    }
}