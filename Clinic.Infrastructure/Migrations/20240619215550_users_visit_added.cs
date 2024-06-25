using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class users_visit_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_AspNetUsers_ApplicationUserId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_ApplicationUserId",
                table: "Visits");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("60367e35-088c-4f51-a804-0d1a05fa04a7"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ee7debd2-9d80-4082-be89-11f344f20549"));

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Visits");

            migrationBuilder.CreateTable(
                name: "ApplicationUserVisit",
                columns: table => new
                {
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserVisit", x => new { x.UsersId, x.VisitsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserVisit_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserVisit_Visits_VisitsId",
                        column: x => x.VisitsId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0159d25d-4112-4843-a2e5-52aadb3ea934"), null, "Doctor", "Doctor" },
                    { new Guid("60932496-2d13-4f69-8fdd-99507aff62f2"), null, "Patient", "Patient" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserVisit_VisitsId",
                table: "ApplicationUserVisit",
                column: "VisitsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserVisit");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0159d25d-4112-4843-a2e5-52aadb3ea934"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("60932496-2d13-4f69-8fdd-99507aff62f2"));

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Visits",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("60367e35-088c-4f51-a804-0d1a05fa04a7"), null, "Doctor", "Doctor" },
                    { new Guid("ee7debd2-9d80-4082-be89-11f344f20549"), null, "Patient", "Patient" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visits_ApplicationUserId",
                table: "Visits",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_AspNetUsers_ApplicationUserId",
                table: "Visits",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
