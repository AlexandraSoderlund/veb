using Datalager.Models;
using System.Data.Entity;

namespace webapp.Models
{
    public class DejtDbContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }


        public DejtDbContext() : base("karleksmums") {

        }
    }
}