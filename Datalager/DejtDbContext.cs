using Datalager.Models;
using System.Data.Entity;

namespace Datalager
{
    public class DejtDbContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
      

        public DejtDbContext() : base("karleksmums") {

        }
    }
}