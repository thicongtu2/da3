using Microsoft.EntityFrameworkCore.Migrations;

namespace Da3.Infrastructure.Migrations
{
    public partial class AddPhoneAndDa3UserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "employers",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Da3UserId",
                table: "aspnetusers",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Da3UserId",
                table: "aspnetuserroles",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_aspnetuserroles_Da3UserId",
                table: "aspnetuserroles",
                column: "Da3UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetuserroles_aspnetusers_Da3UserId",
                table: "aspnetuserroles",
                column: "Da3UserId",
                principalTable: "aspnetusers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_aspnetuserroles_aspnetusers_Da3UserId",
                table: "aspnetuserroles");

            migrationBuilder.DropIndex(
                name: "IX_aspnetuserroles_Da3UserId",
                table: "aspnetuserroles");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "employers");

            migrationBuilder.DropColumn(
                name: "Da3UserId",
                table: "aspnetusers");

            migrationBuilder.DropColumn(
                name: "Da3UserId",
                table: "aspnetuserroles");
        }
    }
}
