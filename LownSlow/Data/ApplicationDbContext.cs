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
        public DbSet<Food> Food { get; set; }

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

            //Give the admin some ingredients
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient()
                {
                    IngredientId = 1,
                    UserId = admin.Id,
                    Name = "Salt"
                },
                new Ingredient()
                {
                    IngredientId = 2,
                    UserId = admin.Id,
                    Name = "Sugar"
                },
                new Ingredient()
                {
                    IngredientId = 3,
                    UserId = admin.Id,
                    Name = "Pepper"
                },
                new Ingredient()
                {
                    IngredientId = 4,
                    UserId = admin.Id,
                    Name = "Apple Cider Vinegar"
                },
                new Ingredient()
                {
                    IngredientId = 5,
                    UserId = admin.Id,
                    Name = "Cayenne"
                },
                new Ingredient()
                {
                    IngredientId = 6,
                    UserId = admin.Id,
                    Name = "Garlic Powder"
                },
                new Ingredient()
                {
                    IngredientId = 7,
                    UserId = admin.Id,
                    Name = "Onion Powder"
                },
                new Ingredient()
                {
                    IngredientId = 8,
                    UserId = admin.Id,
                    Name = "Whole Chicken"
                },
                new Ingredient()
                {
                    IngredientId = 9,
                    UserId = admin.Id,
                    Name = "Salmon filet"
                }
            );

            //Give the user ingredients
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient()
                {
                    IngredientId = 10,
                    UserId = user.Id,
                    Name = "Better Salt"
                },
                new Ingredient()
                {
                    IngredientId = 11,
                    UserId = user.Id,
                    Name = "Better Sugar"
                },
                new Ingredient()
                {
                    IngredientId = 12,
                    UserId = user.Id,
                    Name = "Better Pepper"
                },
                new Ingredient()
                {
                    IngredientId = 13,
                    UserId = user.Id,
                    Name = "Better Apple Cider Vinegar"
                },
                new Ingredient()
                {
                    IngredientId = 14,
                    UserId = user.Id,
                    Name = "Better Cayenne"
                },
                new Ingredient()
                {
                    IngredientId = 15,
                    UserId = user.Id,
                    Name = "Better Garlic Powder"
                },
                new Ingredient()
                {
                    IngredientId = 16,
                    UserId = user.Id,
                    Name = "Better Onion Powder"
                },
                new Ingredient()
                {
                    IngredientId = 17,
                    UserId = user.Id,
                    Name = "Whole Chicken"
                },
                new Ingredient()
                {
                    IngredientId = 18,
                    UserId = user.Id,
                    Name = "Salamon filet"
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
                    Comment = "Well, it was edible",
                    Favorite = false,
                    TechniqueId = 1,
                    UserId = admin.Id
                },
                new Recipe()
                {
                    RecipeId = 2,
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
                    RecipeId = 3,
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
                    RecipeId = 4,
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
                    Description = "Uses charcoal and wood to smoke food at a temperature between 190F - 350F",
                    UserId = admin.Id
                },
                new Technique()
                {
                    TechniqueId = 2,
                    Name = "Cold Smoke",
                    Description = "Uses wood dust to keep an ember smoldering at a low temperature around 100 - 140F",
                    UserId = admin.Id
                }
            );

            //Give the user some techniques
            modelBuilder.Entity<Technique>().HasData(
                new Technique()
                {
                    TechniqueId = 3,
                    Name = "Regular Smoke",
                    Description = "Uses charcoal and wood to smoke food at a temperature between 190F - 350F",
                    UserId = user.Id
                },
                new Technique()
                {
                    TechniqueId = 4,
                    Name = "Cold Smoke",
                    Description = "Uses powdered wood to keep an ember smoldering at a low temperature around 100 - 140F",
                    UserId = user.Id
                }
            );

            //Make an ingredient list for the admin's first recipe.
            modelBuilder.Entity<IngredientList>().HasData(
                new IngredientList()
                {
                    IngredientListId = 1,
                    Quantity = 10,
                    Measurement = "tsp",
                    RecipeId = 1,
                    IngredientId = 1
                },
                new IngredientList()
                {
                    IngredientListId = 2,
                    Quantity = 5,
                    Measurement = "Tbsp",
                    RecipeId = 1,
                    IngredientId = 2
                },
                new IngredientList()
                {
                    IngredientListId = 3,
                    Quantity = 10,
                    Measurement = "tsp",
                    RecipeId = 1,
                    IngredientId = 3
                },
                new IngredientList()
                {
                    IngredientListId = 4,
                    Quantity = 5,
                    Measurement = "tsp",
                    RecipeId = 1,
                    IngredientId = 4
                },
                new IngredientList()
                {
                    IngredientListId = 5,
                    Quantity = 6,
                    Measurement = "Tbsp",
                    RecipeId = 1,
                    IngredientId = 5
                },
                new IngredientList()
                {
                    IngredientListId = 6,
                    Quantity = 10,
                    Measurement = "Tbsp",
                    RecipeId = 1,
                    IngredientId = 6
                },
                new IngredientList()
                {
                    IngredientListId = 7,
                    Quantity = 10,
                    Measurement = "tsp",
                    RecipeId = 1,
                    IngredientId = 7
                },
                new IngredientList()
                {
                    IngredientListId = 8,
                    Quantity = 1,
                    Measurement = "lbs",
                    RecipeId = 1,
                    IngredientId = 8
                }
            );

            //Make an ingredient list for the admin for their second recipe
            modelBuilder.Entity<IngredientList>().HasData(
                new IngredientList()
                {
                    IngredientListId = 9,
                    Quantity = 10,
                    Measurement = "tsp",
                    RecipeId = 2,
                    IngredientId = 1
                },
                new IngredientList()
                {
                    IngredientListId = 10,
                    Quantity = 5,
                    Measurement = "Tbsp",
                    RecipeId = 2,
                    IngredientId = 2
                },
                new IngredientList()
                {
                    IngredientListId = 11,
                    Quantity = 10,
                    Measurement = "tsp",
                    RecipeId = 2,
                    IngredientId = 3
                },
                new IngredientList()
                {
                    IngredientListId = 12,
                    Quantity = 6,
                    Measurement = "Tbsp",
                    RecipeId = 2,
                    IngredientId = 5
                },
                new IngredientList()
                {
                    IngredientListId = 13,
                    Quantity = 10,
                    Measurement = "Tbsp",
                    RecipeId = 2,
                    IngredientId = 6
                },
                new IngredientList()
                {
                    IngredientListId = 14,
                    Quantity = 10,
                    Measurement = "tsp",
                    RecipeId = 2,
                    IngredientId = 7
                },
                new IngredientList()
                {
                    IngredientListId = 15,
                    Quantity = 10,
                    Measurement = "lbs",
                    RecipeId = 2,
                    IngredientId = 9
                }
            );

            //Make an ingredient list for the user's first recipe.
            modelBuilder.Entity<IngredientList>().HasData(
                new IngredientList()
                {
                    IngredientListId = 16,
                    Quantity = 10,
                    Measurement = "tsp",
                    RecipeId = 3,
                    IngredientId = 10
                },
                new IngredientList()
                {
                    IngredientListId = 17,
                    Quantity = 5,
                    Measurement = "Tbsp",
                    RecipeId = 3,
                    IngredientId = 11
                },
                new IngredientList()
                {
                    IngredientListId = 18,
                    Quantity = 10,
                    Measurement = "tsp",
                    RecipeId = 3,
                    IngredientId = 12
                },
                new IngredientList()
                {
                    IngredientListId = 19,
                    Quantity = 5,
                    Measurement = "tsp",
                    RecipeId = 3,
                    IngredientId = 13
                },
                new IngredientList()
                {
                    IngredientListId = 20,
                    Quantity = 6,
                    Measurement = "Tbsp",
                    RecipeId = 3,
                    IngredientId = 14
                },
                new IngredientList()
                {
                    IngredientListId = 21,
                    Quantity = 10,
                    Measurement = "Tbsp",
                    RecipeId = 3,
                    IngredientId = 15
                },
                new IngredientList()
                {
                    IngredientListId = 22,
                    Quantity = 10,
                    Measurement = "tsp",
                    RecipeId = 3,
                    IngredientId = 16
                },
                new IngredientList()
                {
                    IngredientListId = 23,
                    Quantity = 1,
                    Measurement = "lbs",
                    RecipeId = 3,
                    IngredientId = 17
                }
            );

            //Make an ingredient list for the user's second recipe
            modelBuilder.Entity<IngredientList>().HasData(
                new IngredientList()
                {
                    IngredientListId = 24,
                    Quantity = 10,
                    Measurement = "tsp",
                    RecipeId = 4,
                    IngredientId = 10
                },
                new IngredientList()
                {
                    IngredientListId = 25,
                    Quantity = 5,
                    Measurement = "Tbsp",
                    RecipeId = 4,
                    IngredientId = 11
                },
                new IngredientList()
                {
                    IngredientListId = 26,
                    Quantity = 10,
                    Measurement = "tsp",
                    RecipeId = 4,
                    IngredientId = 12
                },
                new IngredientList()
                {
                    IngredientListId = 27,
                    Quantity = 6,
                    Measurement = "Tbsp",
                    RecipeId = 4,
                    IngredientId = 14
                },
                new IngredientList()
                {
                    IngredientListId = 28,
                    Quantity = 10,
                    Measurement = "Tbsp",
                    RecipeId = 4,
                    IngredientId = 15
                },
                new IngredientList()
                {
                    IngredientListId = 29,
                    Quantity = 10,
                    Measurement = "tsp",
                    RecipeId = 4,
                    IngredientId = 16
                },
                new IngredientList()
                {
                    IngredientListId = 30,
                    Quantity = 1,
                    Measurement = "lbs",
                    RecipeId = 4,
                    IngredientId = 18
                }
            );

            modelBuilder.Entity<Food>().HasData(
                new Food()
                {
                    FoodId = 1,
                    Name = "Back ribs",
                    Description = "This cut comes from the back of the cow.",
                    Type = "Beef",
                    MinCookTime = 180,
                    MaxCookTime = 240,
                    MinCookTemp = 225,
                    MaxCookTemp = 250,
                    MinFinishedTemp = 190,
                    MaxFinishedTemp = 205
                }, 
                new Food()
                {
                    FoodId = 2,
                    Name = "Brisket",
                    Description = "A large slab of beef.",
                    Type = "Beef",
                    MinCookTime = 720,
                    MaxCookTime = 1200,
                    MinCookTemp = 225,
                    MaxCookTemp = 250,
                    MinFinishedTemp = 190,
                    MaxFinishedTemp = 205
                },
                new Food()
                {
                    FoodId = 3,
                    Name = "Short Rib",
                    Description = "These ribs are short.",
                    Type = "Beef",
                    MinCookTime = 360,
                    MaxCookTime = 480,
                    MinCookTemp = 225,
                    MaxCookTemp = 250,
                    MinFinishedTemp = 190,
                    MaxFinishedTemp = 200
                },
                new Food()
                {
                    FoodId = 4,
                    Name = "Prime rib",
                    Description = "Tender goodness, there's a reason why it's so expensive.",
                    Type = "Beef",
                    MinCookTime = 60,
                    MaxCookTime = 120,
                    MinCookTemp = 225,
                    MaxCookTemp = 250,
                    MinFinishedTemp = 140,
                    MaxFinishedTemp = 145
                },
                new Food()
                {
                    FoodId = 5,
                    Name = "Tenderloin",
                    Description = "A tender cut of meat.",
                    Type = "Beef",
                    MinCookTime = 150,
                    MaxCookTime = 180,
                    MinCookTemp = 225,
                    MaxCookTemp = 250,
                    MinFinishedTemp = 190,
                    MaxFinishedTemp = 205
                },
                new Food()
                {
                    FoodId = 6,
                    Name = "Baby back ribs",
                    Description = "I want my baby back, baby back ribs.",
                    Type = "Pork",
                    MinCookTime = 300,
                    MaxCookTime = 360,
                    MinCookTemp = 225,
                    MaxCookTemp = 250,
                    MinFinishedTemp = 180,
                    MaxFinishedTemp = 190
                },
                new Food()
                {
                    FoodId = 7,
                    Name = "Belly bacon",
                    Description = "Everyone loves bacon.",
                    Type = "Pork",
                    MinCookTime = 360,
                    MaxCookTime = 390,
                    MinCookTemp = 99,
                    MinFinishedTemp = 140,
                    MaxFinishedTemp = 150
                },
                new Food()
                {
                    FoodId = 8,
                    Name = "Whole hog",
                    Description = "Throw a whole pig on the smoker.",
                    Type = "Pork",
                    MinCookTime = 960,
                    MaxCookTime = 1080,
                    MinCookTemp = 225,
                    MaxCookTemp = 250,
                    MinFinishedTemp = 205,
                    MaxFinishedTemp = 210
                },
                new Food()
                {
                    FoodId = 9,
                    Name = "Sausage",
                    Description = "Pork sausage.",
                    Type = "Pork",
                    MinCookTime = 60,
                    MaxCookTime = 180,
                    MinCookTemp = 225,
                    MaxCookTemp = 250,
                    MinFinishedTemp = 165,
                    MaxFinishedTemp = 170
                },
                new Food()
                {
                    FoodId = 10,
                    Name = "Pork Tenderloin",
                    Description = "A tender cut of pork, hence the name.",
                    Type = "Pork",
                    MinCookTime = 150,
                    MaxCookTime = 180,
                    MinCookTemp = 225,
                    MaxCookTemp = 250,
                    MinFinishedTemp = 160,
                    MaxFinishedTemp = 165
                },
                new Food()
                {
                    FoodId = 11,
                    Name = "Leg",
                    Description = "A leg from a lamb.",
                    Type = "Lamb",
                    MinCookTime = 240,
                    MaxCookTime = 480,
                    MinCookTemp = 225,
                    MaxCookTemp = 250,
                    MinFinishedTemp = 140,
                    MaxFinishedTemp = 150
                },
                new Food()
                {
                    FoodId = 12,
                    Name = "Shoulder",
                    Description = "Lamb shoulder.",
                    Type = "Lamb",
                    MinCookTime = 300,
                    MaxCookTime = 330,
                    MinCookTemp = 225,
                    MaxCookTemp = 250,
                    MinFinishedTemp = 170,
                    MaxFinishedTemp = 175
                },
                new Food()
                {
                    FoodId = 13,
                    Name = "Shank",
                    Description = "Lamb shank.",
                    Type = "Lamb",
                    MinCookTime = 240,
                    MaxCookTime = 300,
                    MinCookTemp = 225,
                    MaxCookTemp = 250,
                    MinFinishedTemp = 190,
                    MaxFinishedTemp = 195
                },
                new Food()
                {
                    FoodId = 14,
                    Name = "Whole Chicken",
                    Description = "A whole bird.",
                    Type = "Poultry",
                    MinCookTime = 180,
                    MaxCookTime = 240,
                    MinCookTemp = 275,
                    MaxCookTemp = 350,
                    MinFinishedTemp = 170,
                    MaxFinishedTemp = 175
                },
                new Food()
                {
                    FoodId = 15,
                    Name = "Chicken thighs",
                    Description = "Dark meat.",
                    Type = "Poultry",
                    MinCookTime = 90,
                    MaxCookTime = 240,
                    MinCookTemp = 275,
                    MaxCookTemp = 350,
                    MinFinishedTemp = 170,
                    MaxFinishedTemp = 175
                },
                new Food()
                {
                    FoodId = 16,
                    Name = "Chicken wings",
                    Description = "White meat.",
                    Type = "Poultry",
                    MinCookTime = 75,
                    MaxCookTime = 90,
                    MinCookTemp = 275,
                    MaxCookTemp = 350,
                    MinFinishedTemp = 170,
                    MaxFinishedTemp = 175
                },
                new Food()
                {
                    FoodId = 17,
                    Name = "Whole turkey",
                    Description = "Gobble Gobble.",
                    Type = "Poultry",
                    MinCookTime = 240,
                    MaxCookTime = 300,
                    MinCookTemp = 275,
                    MaxCookTemp = 350,
                    MinFinishedTemp = 170,
                    MaxFinishedTemp = 175
                },
                new Food()
                {
                    FoodId = 18,
                    Name = "Turkey leg",
                    Description = "Dark meat.",
                    Type = "Poultry",
                    MinCookTime = 120,
                    MaxCookTime = 180,
                    MinCookTemp = 275,
                    MaxCookTemp = 350,
                    MinFinishedTemp = 170,
                    MaxFinishedTemp = 175
                },
                new Food()
                {
                    FoodId = 19,
                    Name = "Turkey breast",
                    Description = "White meat.",
                    Type = "Poultry",
                    MinCookTime = 240,
                    MaxCookTime = 270,
                    MinCookTemp = 275,
                    MaxCookTemp = 350,
                    MinFinishedTemp = 165,
                    MaxFinishedTemp = 175
                },
                new Food()
                {
                    FoodId = 20,
                    Name = "Quail/Pheasant",
                    Description = "Wild birds.",
                    Type = "Poultry",
                    MinCookTime = 60,
                    MaxCookTime = 90,
                    MinCookTemp = 225,
                    MaxCookTemp = 230,
                    MinFinishedTemp = 160,
                    MaxFinishedTemp = 165
                },
                new Food()
                {
                    FoodId = 21,
                    Name = "Whole duck",
                    Description = "A whole bird.",
                    Type = "Poultry",
                    MinCookTime = 240,
                    MinCookTemp = 225,
                    MaxCookTemp = 250,
                    MinFinishedTemp = 165,
                    MaxFinishedTemp = 170
                },
                new Food()
                {
                    FoodId = 22,
                    Name = "Salmon filet",
                    Description = "Wild caught salmon.",
                    Type = "Seafood",
                    MinCookTime = 90,
                    MaxCookTime = 120,
                    MinCookTemp = 275,
                    MaxCookTemp = 350,
                    MinFinishedTemp = 145,
                    MaxFinishedTemp = 150
                },
                new Food()
                {
                    FoodId = 23,
                    Name = "Whole trout",
                    Description = "Wild caught trout.",
                    Type = "Seafood",
                    MinCookTime = 90,
                    MaxCookTime = 120,
                    MinCookTemp = 225,
                    MinFinishedTemp = 145,
                    MaxFinishedTemp = 150
                },
                new Food()
                {
                    FoodId = 24,
                    Name = "Lobster tails",
                    Description = "Smoked lobster tail.",
                    Type = "Seafood",
                    MinCookTime = 45,
                    MaxCookTime = 60,
                    MinCookTemp = 225,
                    MinFinishedTemp = 140,
                    MaxFinishedTemp = 145
                },
                new Food()
                {
                    FoodId = 25,
                    Name = "Oysters",
                    Description = "Everyone loves smoked oysters. Well maybe not everyone.",
                    Type = "Seafood",
                    MinCookTime = 30,
                    MaxCookTime = 40,
                    MinCookTemp = 225,
                    MinFinishedTemp = 160,
                    MaxFinishedTemp = 165
                },
                new Food()
                {
                    FoodId = 26,
                    Name = "Shrimp",
                    Description = "Slow smoked shrimp.",
                    Type = "Seafood",
                    MinCookTime = 20,
                    MaxCookTime = 30 ,
                    MinCookTemp = 225,
                    MinFinishedTemp = 165,
                    MaxFinishedTemp = 170
                },
                new Food()
                {
                    FoodId = 27,
                    Name = "Corn",
                    Description = "Smoked corn.",
                    Type = "Vegetable & Miscellaneous",
                    MinCookTime = 90,
                    MaxCookTime = 120,
                    MinCookTemp = 225,
                    MaxCookTemp = 240

                },
                new Food()
                {
                    FoodId = 28,
                    Name = "Potatoes",
                    Description = "Smoked potatoes.",
                    Type = "Vegetable & Miscellaneous",
                    MinCookTime = 120,
                    MaxCookTime = 180,
                    MinCookTemp = 225,
                    MaxCookTemp = 240
                },
                new Food()
                {
                    FoodId = 29,
                    Name = "Boudin",
                    Description = "Cajun blood sausage.",
                    Type = "Vegetable & Miscellaneous",
                    MinCookTime = 120,
                    MinCookTemp = 225,
                    MaxCookTemp = 240
                },
                new Food()
                {
                    FoodId = 30,
                    Name = "Brats",
                    Description = "Smoked brats.",
                    Type = "Vegetable & Miscellaneous",
                    MinCookTime = 90,
                    MaxCookTime = 120,
                    MinCookTemp = 225,
                    MaxCookTemp = 240
                },
                new Food()
                {
                    FoodId = 31,
                    Name = "Smoked cheese",
                    Description = "This time is pretty much the same for most cheeses.",
                    Type = "Vegetable & Miscellaneous",
                    MinCookTime = 210,
                    MaxCookTime = 240,
                    MinCookTemp = 80,
                    MaxCookTemp = 90
                }
            );
        }
    }
}
