using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newProp_Doctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0ef53d98-f3c2-485c-8357-3ded5466704e"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("18236367-429c-4200-8ea1-92ebf74ecf92"));

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3668c3a9-57d0-4e59-b488-8fb756e7ed2b"), null, "Patient", "Patient" },
                    { new Guid("8c248787-2fd1-44e7-bd55-45ea0b832ef3"), null, "Doctor", "Doctor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3668c3a9-57d0-4e59-b488-8fb756e7ed2b"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8c248787-2fd1-44e7-bd55-45ea0b832ef3"));

            migrationBuilder.DropColumn(
                name: "Profession",
                table: "Doctors");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0ef53d98-f3c2-485c-8357-3ded5466704e"), null, "Doctor", "Doctor" },
                    { new Guid("18236367-429c-4200-8ea1-92ebf74ecf92"), null, "Patient", "Patient" }
                });
        }
    }
}
