using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class doctor_pics_SrcAndPhysical : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Profile",
                table: "Doctors",
                newName: "ProfileSrc");

            migrationBuilder.RenameColumn(
                name: "Pictures",
                table: "Doctors",
                newName: "ProfilePhysical");

            migrationBuilder.AddColumn<string>(
                name: "PicturesPhysical",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PicturesSrc",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("b4d57e20-bd34-487d-a74a-cce974d0811c"), null, "Patient", "Patient" },
                    { new Guid("eef23b95-6a7b-4b1b-8826-77c015df83b1"), null, "Doctor", "Doctor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b4d57e20-bd34-487d-a74a-cce974d0811c"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("eef23b95-6a7b-4b1b-8826-77c015df83b1"));

            migrationBuilder.DropColumn(
                name: "PicturesPhysical",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "PicturesSrc",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "ProfileSrc",
                table: "Doctors",
                newName: "Profile");

            migrationBuilder.RenameColumn(
                name: "ProfilePhysical",
                table: "Doctors",
                newName: "Pictures");

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
    }
}
