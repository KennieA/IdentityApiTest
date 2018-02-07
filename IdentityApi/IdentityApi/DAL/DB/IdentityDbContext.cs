using IdentityApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.DAL.DB
{
    public class IdentityDbContext:DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) ﻿: base(options) { }


        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(c => new { c.RoleId, c.UserId });

            //modelBuilder.Entity<UserRole>()
            //    .HasOne(bc => bc.User)
            //    .WithMany(b => b.UserRoles)
            //    .HasForeignKey(bc => bc.UserId);

            //modelBuilder.Entity<UserRole>()
            //    .HasOne(bc => bc.Role)
            //    .WithMany(c => c.UserRoles)
            //    .HasForeignKey(bc => bc.RoleId);
        }
    }
}
