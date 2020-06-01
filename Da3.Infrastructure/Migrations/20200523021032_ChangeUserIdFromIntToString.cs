using Microsoft.EntityFrameworkCore.Migrations;

namespace Da3.Infrastructure.Migrations
{
    public partial class ChangeUserIdFromIntToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employers_aspnetusers_UserId1",
                table: "employers");

            migrationBuilder.DropIndex(
                name: "IX_employers_UserId1",
                table: "employers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "employers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "employers",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_employers_UserId",
                table: "employers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_employers_aspnetusers_UserId",
                table: "employers",
                column: "UserId",
                principalTable: "aspnetusers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employers_aspnetusers_UserId",
                table: "employers");

            migrationBuilder.DropIndex(
                name: "IX_employers_UserId",
                table: "employers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "employers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

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
    }
}
