using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Andal.Migrations
{
    /// <inheritdoc />
    public partial class altertable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TitileId",
                table: "JobTitles",
                newName: "TitleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TitleId",
                table: "JobTitles",
                newName: "TitileId");
        }
    }
}
