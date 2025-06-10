using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SHM.ProfileService.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class removed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseProfiles_UserProfiles_UserProfileId",
                table: "HouseProfiles");

            migrationBuilder.DropIndex(
                name: "IX_HouseProfiles_UserProfileId",
                table: "HouseProfiles");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "HouseProfiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileId",
                table: "HouseProfiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HouseProfiles_UserProfileId",
                table: "HouseProfiles",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseProfiles_UserProfiles_UserProfileId",
                table: "HouseProfiles",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }
    }
}
