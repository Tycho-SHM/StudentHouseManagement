using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SHM.ProfileService.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    ImgUrl = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    LastUpdatedDateTimeUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HouseProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ImgUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseProfiles_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HouseMemberships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HouseProfileId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseMemberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseMemberships_HouseProfiles_HouseProfileId",
                        column: x => x.HouseProfileId,
                        principalTable: "HouseProfiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HouseMemberships_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitedByUserProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitedUserProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitedToId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invites_HouseProfiles_InvitedToId",
                        column: x => x.InvitedToId,
                        principalTable: "HouseProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invites_UserProfiles_InvitedByUserProfileId",
                        column: x => x.InvitedByUserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invites_UserProfiles_InvitedUserProfileId",
                        column: x => x.InvitedUserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HouseMemberships_HouseProfileId",
                table: "HouseMemberships",
                column: "HouseProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseMemberships_UserProfileId",
                table: "HouseMemberships",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseProfiles_UserProfileId",
                table: "HouseProfiles",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Invites_InvitedByUserProfileId",
                table: "Invites",
                column: "InvitedByUserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Invites_InvitedToId",
                table: "Invites",
                column: "InvitedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Invites_InvitedUserProfileId",
                table: "Invites",
                column: "InvitedUserProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HouseMemberships");

            migrationBuilder.DropTable(
                name: "Invites");

            migrationBuilder.DropTable(
                name: "HouseProfiles");

            migrationBuilder.DropTable(
                name: "UserProfiles");
        }
    }
}
