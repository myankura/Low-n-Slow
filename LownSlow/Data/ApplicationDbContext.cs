using System;
using System.Collections.Generic;
using System.Text;
using LownSlow.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LownSlow.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<IngredientList> IngredientList { get; set; }
        public DbSet<Technique> Technique { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //DO NOT FORGET THIS LINE
            base.OnModelCreating(modelBuilder);


            //Prevent cascading deletion
            modelBuilder.Entity<Recipe>()
                .HasMany(il => il.IngredientLists)
                .WithOne(r => r.Recipe)
                .OnDelete(DeleteBehavior.Restrict);

            //Prevent cascading deletion
            modelBuilder.Entity<Ingredient>()
                .HasMany(il => il.IngredientLists)
                .WithOne(i => i.Ingredient)
                .OnDelete(DeleteBehavior.Restrict);

            //Create a new user for Identity Framework
            ApplicationUser admin = new ApplicationUser
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

            ApplicationUser user = new ApplicationUser
            {
                FirstName = "Michael",
                LastName = "Yankura",
                UserName = "michael@gmail.com",
                NormalizedUserName = "MICHAEL@GMAIL.COM",
                Email = "michael@me.com",
                NormalizedEmail = "MICHAEL@ME.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "j4k6l3k0-d87f-98eh-10kk-2285db796699",
                Id = "11111111-ffff-ffff-ffff-ffffffffffff"
            };

            var passwordHash = new PasswordHasher<ApplicationUser>();
            admin.PasswordHash = passwordHash.HashPassword(admin, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(admin);

            var userPWHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = userPWHash.HashPassword(user, "User123!");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient()
                {
                    IngredientId = 1,
                    UserId = user.Id,
                    Name = "Salt"
                },
                new Ingredient()
                {
                    IngredientId = 2,
                    UserId = user.Id,
                    Name = "Sugar"
                },
                new Ingredient()
                {
                    IngredientId = 3,
                    UserId = user.Id,
                    Name = "Pepper"
                },
                new Ingredient()
                {
                    IngredientId = 4,
                    UserId = user.Id,
                    Name = "Apple Cider Vinegar"
                },
                new Ingredient()
                {
                    IngredientId = 5,
                    UserId = user.Id,
                    Name = "Cayenne"
                },
                new Ingredient()
                {
                    IngredientId = 6,
                    UserId = user.Id,
                    Name = "Garlic Powder"
                },
                new Ingredient()
                {
                    IngredientId = 7,
                    UserId = user.Id,
                    Name = "Onion Powder"
                }
            );

            //Give the admin ingredients
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient()
                {
                    IngredientId = 8,
                    UserId = user.Id,
                    Name = "Better Salt"
                },
                new Ingredient()
                {
                    IngredientId = 9,
                    UserId = user.Id,
                    Name = "Better Sugar"
                },
                new Ingredient()
                {
                    IngredientId = 10,
                    UserId = user.Id,
                    Name = "Better Pepper"
                },
                new Ingredient()
                {
                    IngredientId = 11,
                    UserId = user.Id,
                    Name = "Better Apple Cider Vinegar"
                },
                new Ingredient()
                {
                    IngredientId = 12,
                    UserId = user.Id,
                    Name = "Better Cayenne"
                },
                new Ingredient()
                {
                    IngredientId = 13,
                    UserId = user.Id,
                    Name = "Better Garlic Powder"
                },
                new Ingredient()
                {
                    IngredientId = 14,
                    UserId = user.Id,
                    Name = "Better Onion Powder"
                }
            );

            //Give the admin recipes
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe()
                {
                    RecipeId = 1,
                    Title = "BBQ Chicken",
                    Description = "A bird that has been cooked in smoke for at a low temperature",
                    Directions = "Stuff happens here",
                    Comment = "Well it was edible",
                    Favorite = false,
                    TechniqueId = 1,
                    UserId = admin.Id
                },
                new Recipe()
                {
                    RecipeId = 2,
                    Title = "BBQ Shoulder",
                    Description = "A delicious pork shoulder",
                    Directions = "Stuff happens here",
                    Comment = "It was better than the chicken",
                    Favorite = false,
                    TechniqueId = 1,
                    UserId = admin.Id
                },
                new Recipe()
                {
                    RecipeId = 3,
                    Title = "Smoked Salamon",
                    Description = "Damn that was good",
                    Directions = "Stuff happens here",
                    Comment = "I would recommend this to a friend",
                    Favorite = true,
                    TechniqueId = 2,
                    UserId = admin.Id
                }
            );

            //Give the user recipes
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe()
                {
                    RecipeId = 4,
                    Title = "BBQ Chicken",
                    Description = "A hickory smoked bird bathed in BBQ sauce",
                    Directions = "Stuff happens here",
                    Comment = "Best damn chicken I've ever had",
                    Favorite = false,
                    TechniqueId = 1,
                    UserId = user.Id
                },
                new Recipe()
                {
                    RecipeId = 5,
                    Title = "BBQ Shoulder",
                    Description = "A delicious pork shoulder",
                    Directions = "Stuff happens here",
                    Comment = "User did it right. Would recommend to the Pope!",
                    Favorite = false,
                    TechniqueId = 1,
                    UserId = user.Id
                },
                new Recipe()
                {
                    RecipeId = 6,
                    Title = "Smoked Salamon",
                    Description = "Damn that was good",
                    Directions = "Stuff happens here",
                    Comment = "I would recommend this to a friend",
                    Favorite = true,
                    TechniqueId = 2,
                    UserId = admin.Id
                }
            );

            //Give the admin some techniques
            modelBuilder.Entity<Technique>().HasData(
                new Technique()
                {
                    TechniqueId = 1,
                    Name = "Regular Smoke",
                    Description = "Uses charcoal and wood to smoke a temp no less than 225F",
                    UserId = admin.Id
                },
                new Technique()
                {
                    TechniqueId = 2,
                    Name = "Cold Smoke",
                    Description = "Uses powdered wood to keep an ember smoldering at a low temperature around 140F",
                    UserId = admin.Id
                }
            );

            //Give the user some techniques
            modelBuilder.Entity<Technique>().HasData(
                new Technique()
                {
                    TechniqueId = 3,
                    Name = "Regular Smoke",
                    Description = "Uses charcoal and wood to smoke a temp no less than 225F",
                    UserId = admin.Id
                },
                new Technique()
                {
                    TechniqueId = 4,
                    Name = "Cold Smoke",
                    Description = "Uses wood dust to keep an ember smoldering at a low temperature around 140F",
                    UserId = admin.Id
                }
            );

            //Make a ingredient list for the admin
            modelBuilder.Entity<IngredientList>().HasData(
                new IngredientList()
                {
                    IngredientListId = 1,
                    Quantity = 10,
                    Measurement = "tsp",
                }
            );
        }
    }
}
