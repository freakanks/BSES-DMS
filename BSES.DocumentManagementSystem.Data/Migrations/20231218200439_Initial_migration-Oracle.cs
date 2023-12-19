using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BSES.DocumentManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_migrationOracle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessLogs",
                columns: table => new
                {
                    ID = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DocumentID = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UserId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ActionTaken = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UpdatedUserID = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    RecordStatusCode = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessLogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentID = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    DocumentName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DocumentPath = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Year = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    Category = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    DocumentType = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    DocumentVersion = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DocumentAccessScope = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Users = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    IsArchived = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UpdatedUserID = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    RecordStatusCode = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UserName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SecretKey = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CompanyCode = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UserRight = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    UserAccessScope = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UpdatedUserID = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    RecordStatusCode = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessLogs");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
