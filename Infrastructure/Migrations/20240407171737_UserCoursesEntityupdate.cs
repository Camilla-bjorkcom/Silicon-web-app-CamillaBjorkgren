using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserCoursesEntityupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BigImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBestSeller = table.Column<bool>(type: "bit", nullable: false),
                    IsDigital = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EstimatedHours = table.Column<int>(type: "int", nullable: true),
                    LikeParameter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UserVotes = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseEntity_CategoryEntity_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_CourseId",
                table: "UserCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEntity_CategoryId",
                table: "CourseEntity",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourses_AspNetUsers_UserId",
                table: "UserCourses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourses_CourseEntity_CourseId",
                table: "UserCourses",
                column: "CourseId",
                principalTable: "CourseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCourses_AspNetUsers_UserId",
                table: "UserCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCourses_CourseEntity_CourseId",
                table: "UserCourses");

            migrationBuilder.DropTable(
                name: "CourseEntity");

            migrationBuilder.DropTable(
                name: "CategoryEntity");

            migrationBuilder.DropIndex(
                name: "IX_UserCourses_CourseId",
                table: "UserCourses");
        }
    }
}
