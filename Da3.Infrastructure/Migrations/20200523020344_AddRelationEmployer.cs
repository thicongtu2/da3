using Microsoft.EntityFrameworkCore.Migrations;

namespace Da3.Infrastructure.Migrations
{
    public partial class AddRelationEmployer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "employers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "employers",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_employers_UserId1",
                table: "employers",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_employers_aspnetusers_UserId1",
                table: "employers",
                column: "UserId1",
                principalTable: "aspnetusers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employers_aspnetusers_UserId1",
                table: "employers");

            migrationBuilder.DropIndex(
                name: "IX_employers_UserId1",
                table: "employers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "employers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "employers");
        }
    }
}
