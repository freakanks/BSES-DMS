using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BSES.DocumentManagementSystem.Encryption.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DMSEncryptionKeys",
                columns: table => new
                {
                    CompanyCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EncryptionKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EncryptionIV = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMSEncryptionKeys", x => x.CompanyCode);
                });

            migrationBuilder.InsertData(
                table: "DMSEncryptionKeys",
                columns: new[] { "CompanyCode", "EncryptionIV", "EncryptionKey" },
                values: new object[,]
                {
                    { "BRPL", "CaTOyz86Ad0TGyuWZts27A==", "xyq2FaIYqbcwTuUlsiQ0UbJWAUCfh2JlmJVvwQs5Khg=" },
                    { "BYPL", "G27D2ihSFZmREv17iBL0zQ==", "QxZ5q8IuyjOn+Pyp3nucakA9jaieuKSvYZD8qLwYOqk=" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DMSEncryptionKeys");
        }
    }
}
