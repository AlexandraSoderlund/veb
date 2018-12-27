using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace veb.Models
{
    public class RegistreraNyAnvändareDBContext: DbContext 
    {
        public DbSet<RegistreraNyAnvändare> Användare { get; set; }

        public RegistreraNyAnvändareDBContext() : base("användareDb")
        {

        }
    }
}