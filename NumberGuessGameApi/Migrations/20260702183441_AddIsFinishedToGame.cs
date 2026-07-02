using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NumberGuessGameApi.Migrations
{
    /// <inheritdoc />
    public partial class AddIsFinishedToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Finished",
                table: "Games",
                newName: "IsFinished");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsFinished",
                table: "Games",
                newName: "Finished");
        }
    }
}
