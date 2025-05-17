using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aSati.Migrations
{
    /// <inheritdoc />
    public partial class JustAcheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PropertyId1",
                table: "PropertyUnits",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyUnitId1",
                table: "Leases",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyUnits_PropertyId1",
                table: "PropertyUnits",
                column: "PropertyId1");

            migrationBuilder.CreateIndex(
                name: "IX_Leases_PropertyUnitId1",
                table: "Leases",
                column: "PropertyUnitId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Leases_PropertyUnits_PropertyUnitId1",
                table: "Leases",
                column: "PropertyUnitId1",
                principalTable: "PropertyUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyUnits_Properties_PropertyId1",
                table: "PropertyUnits",
                column: "PropertyId1",
                principalTable: "Properties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leases_PropertyUnits_PropertyUnitId1",
                table: "Leases");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyUnits_Properties_PropertyId1",
                table: "PropertyUnits");

            migrationBuilder.DropIndex(
                name: "IX_PropertyUnits_PropertyId1",
                table: "PropertyUnits");

            migrationBuilder.DropIndex(
                name: "IX_Leases_PropertyUnitId1",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "PropertyId1",
                table: "PropertyUnits");

            migrationBuilder.DropColumn(
                name: "PropertyUnitId1",
                table: "Leases");
        }
    }
}
