﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WellnessClimbingWall.Auth;
using WellnessClimbingWall.Models;

namespace WellnessClimbingWall.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Route> Route {get; set;}
        public DbSet<Patron> Patron { get; set; }
        public DbSet<Visit> Visit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seed our routes
            modelBuilder.Entity<Route>().HasData(new Route
            { 
                RouteId = 1, 
                Grade = "5.5", 
                Color = "Blue", 
                DateCreated = new DateTime(2021, 1, 7), 
                Location = "None", 
                Rope = 21, 
                Setter = "John" 
            });

            modelBuilder.Entity<Route>().HasData(new Route 
            {   
                RouteId = 2, 
                Grade = "8.2", 
                Color = "Red",
                DateCreated = new DateTime(2021, 1, 10), 
                Location = "None", 
                Rope = 6, 
                Setter = "Jack"
            });

            modelBuilder.Entity<Route>().HasData(new Route 
            { 
                RouteId = 3, 
                Grade = "4.9", 
                Color = "Yellow", 
                DateCreated = new DateTime(2021, 1, 3), 
                Location = "North Wall", 
                Rope = 0, 
                Setter = "Charlie"
            });

            modelBuilder.Entity<Route>().HasData(new Route
            { 
                RouteId = 4, 
                Grade = "9.2", 
                Color = "Black", 
                DateCreated = new DateTime(2021, 1, 22), 
                Location = "None", 
                Rope = 11, 
                Setter = "Stacy"
            });

            modelBuilder.Entity<Visit>().HasData(new Visit
            {
                ID = 112233,
                Name = "Chester",
                Certifications = "Belay",
                timeIn = DateTime.Now,
                timeOut = DateTime.Now,
            });

            modelBuilder.Entity<Patron>().HasData(new Patron
            {
                ID = 1,
                BadgeNumber = 1234567,
                Certifications = "None",
                Name = "Miller Wallman",
            });

            var ROLE_ID = Guid.NewGuid().ToString();
            var USER_ID = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Administrators",
                NormalizedName = "Admin",
                Id = ROLE_ID,
            });

            //create user
            var appUser = new ApplicationUser
            {
                Id = USER_ID,
                UserName = "Administrator",
                Email = "temp@admin.com",
                NormalizedUserName = "Administrator",
           };

            //set user password
            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "Password1!");

            //seed user
            modelBuilder.Entity<ApplicationUser>().HasData(appUser);

            //set user role to admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = USER_ID
            });
        }
    }
}