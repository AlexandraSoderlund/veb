﻿using Datalager.Models;
using System.Data.Entity;

namespace Datalager
{
    public class DejtDbContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Post> Posts { get; set; }


        public DejtDbContext() : base("karleksmums")
        {

        }

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
        }

    }
}