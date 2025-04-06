﻿using HomeOwners.Models;
using HomeOwners.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Net;

namespace HomeOwners.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        /**
         * Auto-generated Tables.
         */
        public DbSet<Facility> Facility { get; set; }
        public DbSet<Billing> Billing { get; set; }
        public DbSet<Event> Events { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /**
             * User[One] <-> Billing[Many]
             */
            builder.Entity<User>()
                .HasMany(e => e.Billings)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            // seeding table operations. remove in production
            SeedTables(builder);
        }

        private void SeedTables(ModelBuilder builder)
        {
            // password hashing bullshit
            var hasher = new PasswordHasher<User>();

            // Role seeding
            var adminRole = new IdentityRole
            {
                Id = "role-admin-0001",
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            var userRole = new IdentityRole
            {
                Id = "role-user-0001",
                Name = "User",
                NormalizedName = "USER"
            };
            builder.Entity<IdentityRole>().HasData(adminRole);
            builder.Entity<IdentityRole>().HasData(userRole);

            // Creating user
            // User account
            var user = new User
            {
                Id = "test-user-0001",
                Email = "user@email.com",
                NormalizedEmail = "USER@EMAIL.COM",
                UserName = "user@email.com",
                NormalizedUserName = "USER@EMAIL.COM",
                LastName = "Doe",
                FirstName = "John",
                MidInitial = "A",
                Address = "123 User St."
            };
            user.PasswordHash = hasher.HashPassword(user, "password");
            builder.Entity<User>().HasData(user);
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = userRole.Id,
                    UserId = user.Id,
                });
            // Admin Account
            var admin = new User
            {
                Id = "test-admin-0001",
                Email = "admin@email.com",
                NormalizedEmail = "ADMIN@EMAIL.COM",
                UserName = "admin@email.com",
                NormalizedUserName = "ADMIN@EMAIL.COM",
                LastName = "Doe",
                FirstName = "John",
                MidInitial = "A",
                Address = "123 User St."
            };
            admin.PasswordHash = hasher.HashPassword(user, "password");
            builder.Entity<User>().HasData(admin);
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRole.Id,
                    UserId = admin.Id,
                });

            // Facility seeding
            builder.Entity<Facility>().HasData([
                new Facility
                {
                    Id = "test-facility-0001",
                    Name = "Facility Name",
                    Description = "Tell me about this facility",
                    Address = "Somewhere, i don't really know."
                },
                new Facility
                {
                    Id = "test-facility-0002",
                    Name = "Facility Name",
                    Description = "Tell me about this facility",
                    Address = "Somewhere, i don't really know."
                },
                new Facility
                {
                    Id = "test-facility-0003",
                    Name = "Facility Name",
                    Description = "Tell me about this facility",
                    Address = "Somewhere, i don't really know."
                },
                new Facility
                {
                    Id = "test-facility-0004",
                    Name = "Facility Name",
                    Description = "Tell me about this facility",
                    Address = "Somewhere, i don't really know."
                },
                new Facility
                {
                    Id = "test-facility-0005",
                    Name = "Facility Name",
                    Description = "Tell me about this facility",
                    Address = "Somewhere, i don't really know."
                },
                new Facility
                {
                    Id = "test-facility-0006",
                    Name = "Facility Name",
                    Description = "Tell me about this facility",
                    Address = "Somewhere, i don't really know."
                },
                ]);

            // Billing Seeding for user@email.com
            builder.Entity<Billing>().HasData([
                new Billing
                {
                    Id = "test-billing-0001",
                    Name = "Rent",
                    IsPaid = false,
                    Amount = 2000.00,
                    UserId = "test-user-0001",
                    IssuedAt = new DateOnly(2025, 4, 2)
                },
                new Billing
                {
                    Id = "test-billing-0002",
                    Name = "Electricity",
                    IsPaid = false,
                    Amount = 150.00,
                    UserId = "test-user-0001",
                    IssuedAt = new DateOnly(2025, 4, 2)
                }
                ]);
        }
    }


}