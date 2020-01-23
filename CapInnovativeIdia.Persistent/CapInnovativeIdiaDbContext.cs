using CapInnovativeIdia.Domain.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapInnovativeIdia.Persistent
{
    public class CapInnovativeIdiaDbContext : DbContext
    {
        public CapInnovativeIdiaDbContext()
        {

        }
        public CapInnovativeIdiaDbContext(DbContextOptions<CapInnovativeIdiaDbContext> options)
           : base(options)
        {
        }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<UserRoleMapping> UserRoleMapping { get; set; }
        public virtual DbSet<Idia> Idia { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<Controller> Controller { get; set; }
        public virtual DbSet<ControllerAction> ControllerAction { get; set; }
        public virtual DbSet<IdiaCategory> IdiaCategory { get; set; }
        public virtual DbSet<IdiaProposal> IdiaProposal { get; set; }
        public virtual DbSet<IdiaStatus> IdiaStatus { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=Sandilya@123;database=CapInnovativeIdia");
                }
            }
            catch (Exception ex)
            {
                
            } 
        }
    }
}
