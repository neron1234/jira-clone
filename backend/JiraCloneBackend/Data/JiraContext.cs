using JiraCloneBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiraCloneBackend.Data
{
    public class JiraContext: DbContext
    {

        public DbSet<Project> Projects { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<User> Users { get; set; }


        public JiraContext(DbContextOptions options)
            : base(options)
        {
        }

        //Configure data models here - configurations here override annotations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            /*
             * tried to use .HasDefaultValue(DatTime.Now) but that just
             * sets the default date value to the time that the code ran,
             * it needs to set on create - for now just put date gen in route
             */
            modelBuilder.Entity<Issue>()
                .Property(d => d.Created)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<User>()
                .Property(u => u.UserCreated)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Project>()
                .Property(p => p.Created)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Issue>()
                .Property(i => i.Updated)
                .ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Project>()
                .Property(p => p.Updated)
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

        }
    }
}
