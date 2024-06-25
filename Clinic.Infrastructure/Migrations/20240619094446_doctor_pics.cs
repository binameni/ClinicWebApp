using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class doctor_pics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0fa7d6b8-62f4-4116-83fe-e3648d954f63"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("59fb6e05-9984-471b-a7ed-e410b1ba2b72"));

            migrationBuilder.AddColumn<string>(
                name: "Pictures",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Profile",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4bfd2773-067f-4802-b85b-ab5225454e14"), null, "Doctor", "Doctor" },
                    { new Guid("cf731f70-ddba-45d8-b8d7-8c297d2c64c1"), null, "Patient", "Patient" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4bfd2773-067f-4802-b85b-ab5225454e14"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cf731f70-ddba-45d8-b8d7-8c297d2c64c1"));

            migrationBuilder.DropColumn(
                name: "Pictures",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Profile",
                table: "Doctors");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0fa7d6b8-62f4-4116-83fe-e3648d954f63"), null, "Doctor", "Doctor" },
                    { new Guid("59fb6e05-9984-471b-a7ed-e410b1ba2b72"), null, "Patient", "Patient" }
                });
        }
    }
}
