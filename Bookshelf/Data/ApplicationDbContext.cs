using System;
using System.Collections.Generic;
using System.Text;
using Bookshelf.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Books)
                .WithOne(b => b.Owner)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Author>()
                .Property("FirstName")
                .HasDefaultValue("Rufus");

            // Create a new user for Identity Framework
            ApplicationUser user = new ApplicationUser
            {
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            builder.Entity<ApplicationUser>().HasData(user);

            Author hermanMelville = new Author()
            {
                Id = 1,
                FirstName = "Herman",
                LastName = "Melville",
                PreferredGenre = "Whale Allegories",
                UserCreatingId = user.Id
            };
            builder.Entity<Author>().HasData(hermanMelville);

            Book mobyDick = new Book()
            {
                Id = 1,
                Title = "Moby Dick",
                ISBN = "9092939449",
                Genre = "Whale Allegories",
                PublishDate = new DateTime(1851, 11, 14),
                AuthorId = hermanMelville.Id,
                OwnerId = user.Id
            };
            builder.Entity<Book>().HasData(mobyDick);

        }
    }
}
