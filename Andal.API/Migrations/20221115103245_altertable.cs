using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Andal.Migrations
{
    /// <inheritdoc />
    public partial class altertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "JobTitles",
                newName: "TitileId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "JobPositions",
                newName: "PositionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TitileId",
                table: "JobTitles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "JobPositions",
                newName: "Id");
        }
    }
}
