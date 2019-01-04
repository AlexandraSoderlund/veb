using Datalager.Models;
using System.Data.Entity;
// skapat en egen databas
namespace Datalager
{
    public class DejtDbContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<FriendsRequest> Förfrågan { get; set; }


        public DejtDbContext() : base("karleksmums")
        {

        }

        // Här kan man hjälpa Entity Framework om hur databasen hänger ihop.
        // I vårat fall är det omöjligt för Entity Framework att lista ut vilket 
        // fält som hör till vilket eftersom vi har flera fält på till exempelvis Post som hör
        // till användare
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasRequired(s => s.Avsändare)
                .WithMany(x => x.AvsändarePosts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasRequired(s => s.Mottagare)
                .WithMany(x => x.MottagarePosts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FriendsRequest>()
                .HasRequired(s => s.Avsändare)
                .WithMany(x => x.AvsändareFörfrågan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FriendsRequest>()
                .HasRequired(s => s.Mottagare)
                .WithMany(x => x.Mottagareförfrågan)
                .WillCascadeOnDelete(false);
        }

    }
}