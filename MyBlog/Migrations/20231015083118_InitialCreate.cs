using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlog.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "option",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    value = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_option", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    parentId = table.Column<int>(type: "INTEGER", nullable: false),
                    userId = table.Column<int>(type: "INTEGER", nullable: false),
                    publishDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    content = table.Column<string>(type: "TEXT", nullable: false),
                    filteredContent = table.Column<string>(type: "TEXT", nullable: false),
                    status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    path = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ogTitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ogDescription = table.Column<string>(type: "TEXT", nullable: false),
                    ogKeywords = table.Column<string>(type: "TEXT", nullable: false),
                    ogImage = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    displayId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    createDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    role = table.Column<int>(type: "INTEGER", nullable: false),
                    image = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    about = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "option");

            migrationBuilder.DropTable(
                name: "post");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
