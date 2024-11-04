using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Project.Migrations
{
    /// <inheritdoc />
    public partial class updateTetio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Testimonials_AspNetUsers_UserId1",
                table: "Testimonials");

            migrationBuilder.DropIndex(
                name: "IX_Testimonials_UserId1",
                table: "Testimonials");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Testimonials");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Testimonials",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAnonymous",
                table: "Testimonials",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Testimonials",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Testimonials_UserId",
                table: "Testimonials",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Testimonials_AspNetUsers_UserId",
                table: "Testimonials",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Testimonials_AspNetUsers_UserId",
                table: "Testimonials");

            migrationBuilder.DropIndex(
                name: "IX_Testimonials_UserId",
                table: "Testimonials");

            migrationBuilder.DropColumn(
                name: "IsAnonymous",
                table: "Testimonials");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Testimonials");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Testimonials",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Testimonials",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Testimonials_UserId1",
                table: "Testimonials",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Testimonials_AspNetUsers_UserId1",
                table: "Testimonials",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
