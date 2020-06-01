using Microsoft.EntityFrameworkCore.Migrations;

namespace Da3.Infrastructure.Migrations
{
    public partial class ChangeJobTableTypoError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_jobs_employers_EmployerId",
                table: "jobs");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "jobs");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "jobs",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Salary",
                table: "jobs",
                type: "varchar(255)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployerId",
                table: "jobs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "jobs",
                type: "varchar(255)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_jobs_employers_EmployerId",
                table: "jobs",
                column: "EmployerId",
                principalTable: "employers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_jobs_employers_EmployerId",
                table: "jobs");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "jobs",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Salary",
                table: "jobs",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "EmployerId",
                table: "jobs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "jobs",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 2000);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "jobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_jobs_employers_EmployerId",
                table: "jobs",
                column: "EmployerId",
                principalTable: "employers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
