using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace veb.Models
{
    public class DejtDbContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }


        public DejtDbContext() : base("dejtdb") {

        }
    }
}