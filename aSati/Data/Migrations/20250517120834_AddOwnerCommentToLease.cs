using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aSati.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerCommentToLease : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PropertyChecklistItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OwnerComment",
                table: "Leases",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PropertyChecklistItem");

            migrationBuilder.DropColumn(
                name: "OwnerComment",
                table: "Leases");
        }
    }
}
