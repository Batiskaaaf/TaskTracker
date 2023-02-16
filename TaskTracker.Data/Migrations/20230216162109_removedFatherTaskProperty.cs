using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTracker.Data.Migrations
{
    public partial class removedFatherTaskProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Tasks_FatherTaskId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_FatherTaskId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "FatherTaskId",
                table: "Tasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FatherTaskId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_FatherTaskId",
                table: "Tasks",
                column: "FatherTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Tasks_FatherTaskId",
                table: "Tasks",
                column: "FatherTaskId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
