using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentalMathTelegramBot.Controllers.Migrations
{
    /// <inheritdoc />
    public partial class TestAnswer_ADD_TestType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestType",
                table: "Answers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestType",
                table: "Answers");
        }
    }
}
