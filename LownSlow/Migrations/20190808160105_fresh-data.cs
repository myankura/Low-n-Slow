using Microsoft.EntityFrameworkCore.Migrations;

namespace LownSlow.Migrations
{
    public partial class freshdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ec50e2c4-4096-49d9-b760-276f05c62929", "AQAAAAEAACcQAAAAEJc/14VaO8djiajR+hqh+Zs7G7gtsQhA6W+rtOsfC/8f08IOrIm7wnBzJvfMqnro+g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f6559c03-1e9f-46aa-baef-c5185975cb42", "AQAAAAEAACcQAAAAEA6uwRND0pSQfRfo0Lqj3Doe26U+p+7tMd3Ukn6QWoPlnmxdFMOj7m0k+iNvMOc8PA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aa07fd78-9075-4b78-85b1-1a2e4f00c978", "AQAAAAEAACcQAAAAEPrj+H2mlG98S73f9yB7wP3rUS8VRVudSKszvjzHlbN7QN5/IoeSRFEVtDhoDAcOzQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1c64c9cb-0d65-4f56-887c-93e9f47da6cf", "AQAAAAEAACcQAAAAEPK5VNoR2ystb+I5TqzO0x9Iz9DIRbmz/yTZi50lKn338HrutKcUABZKPxgYcwSnzQ==" });
        }
    }
}
