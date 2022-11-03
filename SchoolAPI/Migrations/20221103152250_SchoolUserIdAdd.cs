using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAPI.Migrations
{
    public partial class SchoolUserIdAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Schools",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Schools_CreatedById",
                table: "Schools",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Teachers_CreatedById",
                table: "Schools",
                column: "CreatedById",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Teachers_CreatedById",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_CreatedById",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Schools");
        }
    }
}
