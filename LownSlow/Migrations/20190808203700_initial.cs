using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LownSlow.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    FoodId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    MinCookTime = table.Column<int>(nullable: false),
                    MaxCookTime = table.Column<int>(nullable: false),
                    MinCookTemp = table.Column<int>(nullable: false),
                    MaxCookTemp = table.Column<int>(nullable: false),
                    MinFinishedTemp = table.Column<int>(nullable: false),
                    MaxFinishedTemp = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.FoodId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    IngredientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.IngredientId);
                    table.ForeignKey(
                        name: "FK_Ingredient_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Technique",
                columns: table => new
                {
                    TechniqueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technique", x => x.TechniqueId);
                    table.ForeignKey(
                        name: "FK_Technique_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    RecipeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Directions = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Favorite = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    TechniqueId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.RecipeId);
                    table.ForeignKey(
                        name: "FK_Recipe_Technique_TechniqueId",
                        column: x => x.TechniqueId,
                        principalTable: "Technique",
                        principalColumn: "TechniqueId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recipe_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientList",
                columns: table => new
                {
                    IngredientListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<int>(nullable: false),
                    Measurement = table.Column<string>(nullable: false),
                    IngredientId = table.Column<int>(nullable: false),
                    RecipeId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientList", x => x.IngredientListId);
                    table.ForeignKey(
                        name: "FK_IngredientList_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IngredientList_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredient",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IngredientList_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "00000000-ffff-ffff-ffff-ffffffffffff", 0, "9fb6f5ac-4b51-4e13-ab6b-4272ce56f264", "admin@admin.com", true, "admin", "admin", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEG/fj3/zM7mWC9WgsIAPVgjjnZxHaQluMH4RzNltyeOJYAglbsBVovc3zCELUU2ACw==", null, false, "7f434309-a4d9-48e9-9ebb-8803db794577", false, "admin@admin.com" },
                    { "11111111-ffff-ffff-ffff-ffffffffffff", 0, "248c1ed3-ffd7-45ab-ad00-e8efed8356f6", "michael@me.com", true, "Michael", "Yankura", false, null, "MICHAEL@ME.COM", "MICHAEL@GMAIL.COM", "AQAAAAEAACcQAAAAEN3ylYMrpTxiKPK26E1l9Le8UUCqzYfPMgUDd2HjArwfQjP7+p8wae0YgY7caa7hxw==", null, false, "j4k6l3k0-d87f-98eh-10kk-2285db796699", false, "michael@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Food",
                columns: new[] { "FoodId", "Description", "MaxCookTemp", "MaxCookTime", "MaxFinishedTemp", "MinCookTemp", "MinCookTime", "MinFinishedTemp", "Name", "Type" },
                values: new object[,]
                {
                    { 29, "Cajun blood sausage.", 240, 0, 0, 225, 120, 0, "Boudin", "Vegetable & Miscellaneous" },
                    { 28, "Smoked potatoes.", 240, 180, 0, 225, 120, 0, "Potatoes", "Vegetable & Miscellaneous" },
                    { 27, "Smoked corn.", 240, 120, 0, 225, 90, 0, "Corn", "Vegetable & Miscellaneous" },
                    { 26, "Slow smoked shrimp.", 0, 30, 170, 225, 20, 165, "Shrimp", "Seafood" },
                    { 25, "Everyone loves smoked oysters. Well maybe not everyone.", 0, 40, 165, 225, 30, 160, "Oysters", "Seafood" },
                    { 24, "Smoked lobster tail.", 0, 60, 145, 225, 45, 140, "Lobster tails", "Seafood" },
                    { 23, "Wild caught trout.", 0, 120, 150, 225, 90, 145, "Whole trout", "Seafood" },
                    { 22, "Wild caught salmon.", 350, 120, 150, 275, 90, 145, "Salmon filet", "Seafood" },
                    { 21, "A whole bird.", 250, 0, 170, 225, 240, 165, "Whole duck", "Poultry" },
                    { 20, "Wild birds.", 230, 90, 165, 225, 60, 160, "Quail/Pheasant", "Poultry" },
                    { 19, "White meat.", 350, 270, 175, 275, 240, 165, "Turkey breast", "Poultry" },
                    { 18, "Dark meat.", 350, 180, 175, 275, 120, 170, "Turkey leg", "Poultry" },
                    { 17, "Gobble Gobble.", 350, 300, 175, 275, 240, 170, "Whole turkey", "Poultry" },
                    { 16, "White meat.", 350, 90, 175, 275, 75, 170, "Chicken wings", "Poultry" },
                    { 15, "Dark meat.", 350, 240, 175, 275, 90, 170, "Chicken thighs", "Poultry" },
                    { 14, "A whole bird.", 350, 240, 175, 275, 180, 170, "Whole Chicken", "Poultry" },
                    { 13, "Lamb shank.", 250, 300, 195, 225, 240, 190, "Shank", "Lamb" },
                    { 12, "Lamb shoulder.", 250, 330, 175, 225, 300, 170, "Shoulder", "Lamb" },
                    { 11, "A leg from a lamb.", 250, 480, 150, 225, 240, 140, "Leg", "Lamb" },
                    { 10, "A tender cut of pork, hence the name.", 250, 180, 165, 225, 150, 160, "Pork Tenderloin", "Pork" },
                    { 9, "Pork sausage.", 250, 180, 170, 225, 60, 165, "Sausage", "Pork" },
                    { 8, "Throw a whole pig on the smoker.", 250, 1080, 210, 225, 960, 205, "Whole hog", "Pork" },
                    { 7, "Everyone loves bacon.", 0, 390, 150, 99, 360, 140, "Belly bacon", "Pork" },
                    { 6, "I want my baby back, baby back ribs.", 250, 360, 190, 225, 300, 180, "Baby back ribs", "Pork" },
                    { 5, "A tender cut of meat.", 250, 180, 205, 225, 150, 190, "Tenderloin", "Beef" },
                    { 4, "Tender goodness, there's a reason why it's so expensive.", 250, 120, 145, 225, 60, 140, "Prime rib", "Beef" },
                    { 3, "These ribs are short.", 250, 480, 200, 225, 360, 190, "Short Rib", "Beef" },
                    { 2, "A large slab of beef.", 250, 1200, 205, 225, 720, 190, "Brisket", "Beef" },
                    { 1, "This cut comes from the back of the cow.", 250, 240, 205, 225, 180, 190, "Back ribs", "Beef" },
                    { 30, "Smoked brats.", 240, 120, 0, 225, 90, 0, "Brats", "Vegetable & Miscellaneous" },
                    { 31, "This time is pretty much the same for most cheeses.", 90, 240, 0, 80, 210, 0, "Smoked cheese", "Vegetable & Miscellaneous" }
                });

            migrationBuilder.InsertData(
                table: "Ingredient",
                columns: new[] { "IngredientId", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "Salt", "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 14, "Better Cayenne", "11111111-ffff-ffff-ffff-ffffffffffff" },
                    { 13, "Better Apple Cider Vinegar", "11111111-ffff-ffff-ffff-ffffffffffff" },
                    { 12, "Better Pepper", "11111111-ffff-ffff-ffff-ffffffffffff" },
                    { 11, "Better Sugar", "11111111-ffff-ffff-ffff-ffffffffffff" },
                    { 10, "Better Salt", "11111111-ffff-ffff-ffff-ffffffffffff" },
                    { 17, "Whole Chicken", "11111111-ffff-ffff-ffff-ffffffffffff" },
                    { 18, "Salamon filet", "11111111-ffff-ffff-ffff-ffffffffffff" },
                    { 9, "Salmon filet", "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 8, "Whole Chicken", "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 7, "Onion Powder", "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 6, "Garlic Powder", "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 5, "Cayenne", "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 4, "Apple Cider Vinegar", "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 3, "Pepper", "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 2, "Sugar", "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 15, "Better Garlic Powder", "11111111-ffff-ffff-ffff-ffffffffffff" },
                    { 16, "Better Onion Powder", "11111111-ffff-ffff-ffff-ffffffffffff" }
                });

            migrationBuilder.InsertData(
                table: "Technique",
                columns: new[] { "TechniqueId", "Description", "Name", "UserId" },
                values: new object[,]
                {
                    { 2, "Uses wood dust to keep an ember smoldering at a low temperature around 100 - 140F", "Cold Smoke", "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 1, "Uses charcoal and wood to smoke food at a temperature between 190F - 350F", "Regular Smoke", "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 3, "Uses charcoal and wood to smoke food at a temperature between 190F - 350F", "Regular Smoke", "11111111-ffff-ffff-ffff-ffffffffffff" },
                    { 4, "Uses powdered wood to keep an ember smoldering at a low temperature around 100 - 140F", "Cold Smoke", "11111111-ffff-ffff-ffff-ffffffffffff" }
                });

            migrationBuilder.InsertData(
                table: "Recipe",
                columns: new[] { "RecipeId", "Comment", "Description", "Directions", "Favorite", "TechniqueId", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "Well, it was edible", "A bird that has been cooked in smoke for at a low temperature", "Stuff happens here", false, 1, "BBQ Chicken", "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 3, "Best damn chicken I've ever had", "A hickory smoked bird bathed in BBQ sauce", "Stuff happens here", false, 1, "BBQ Chicken", "11111111-ffff-ffff-ffff-ffffffffffff" },
                    { 2, "I would recommend this to a friend", "Damn that was good", "Stuff happens here", true, 2, "Smoked Salamon", "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 4, "I would recommend this to a friend", "Damn that was good", "Stuff happens here", true, 2, "Smoked Salamon", "11111111-ffff-ffff-ffff-ffffffffffff" }
                });

            migrationBuilder.InsertData(
                table: "IngredientList",
                columns: new[] { "IngredientListId", "ApplicationUserId", "IngredientId", "Measurement", "Quantity", "RecipeId" },
                values: new object[,]
                {
                    { 1, null, 1, "tsp", 10, 1 },
                    { 28, null, 15, "Tbsp", 10, 4 },
                    { 27, null, 14, "Tbsp", 6, 4 },
                    { 26, null, 12, "tsp", 10, 4 },
                    { 25, null, 11, "Tbsp", 5, 4 },
                    { 24, null, 10, "tsp", 10, 4 },
                    { 15, null, 9, "lbs", 10, 2 },
                    { 14, null, 7, "tsp", 10, 2 },
                    { 13, null, 6, "Tbsp", 10, 2 },
                    { 12, null, 5, "Tbsp", 6, 2 },
                    { 11, null, 3, "tsp", 10, 2 },
                    { 10, null, 2, "Tbsp", 5, 2 },
                    { 9, null, 1, "tsp", 10, 2 },
                    { 23, null, 17, "lbs", 1, 3 },
                    { 22, null, 16, "tsp", 10, 3 },
                    { 21, null, 15, "Tbsp", 10, 3 },
                    { 20, null, 14, "Tbsp", 6, 3 },
                    { 19, null, 13, "tsp", 5, 3 },
                    { 18, null, 12, "tsp", 10, 3 },
                    { 17, null, 11, "Tbsp", 5, 3 },
                    { 16, null, 10, "tsp", 10, 3 },
                    { 8, null, 8, "lbs", 1, 1 },
                    { 7, null, 7, "tsp", 10, 1 },
                    { 6, null, 6, "Tbsp", 10, 1 },
                    { 5, null, 5, "Tbsp", 6, 1 },
                    { 4, null, 4, "tsp", 5, 1 },
                    { 3, null, 3, "tsp", 10, 1 },
                    { 2, null, 2, "Tbsp", 5, 1 },
                    { 29, null, 16, "tsp", 10, 4 },
                    { 30, null, 18, "lbs", 1, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_UserId",
                table: "Ingredient",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientList_ApplicationUserId",
                table: "IngredientList",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientList_IngredientId",
                table: "IngredientList",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientList_RecipeId",
                table: "IngredientList",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_TechniqueId",
                table: "Recipe",
                column: "TechniqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_UserId",
                table: "Recipe",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Technique_UserId",
                table: "Technique",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "IngredientList");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "Technique");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
