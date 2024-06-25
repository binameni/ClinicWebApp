using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class doctorKey_addedKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("04ddaf63-53ca-460f-b11c-0986a9a50e14"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7aebead1-598a-4bcf-b282-fed0268e86f4"));

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "DoctorKey",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorKey",
                table: "DoctorKey",
                column: "Key");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("a45c2889-499c-4d3f-a708-9ba5dbcdcd57"), null, "Patient", "Patient" },
                    { new Guid("ae464d76-af51-465d-922e-7c4a6d1b9e2c"), null, "Doctor", "Doctor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorKey",
                table: "DoctorKey");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a45c2889-499c-4d3f-a708-9ba5dbcdcd57"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ae464d76-af51-465d-922e-7c4a6d1b9e2c"));

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "DoctorKey",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("04ddaf63-53ca-460f-b11c-0986a9a50e14"), null, "Doctor", "Doctor" },
                    { new Guid("7aebead1-598a-4bcf-b282-fed0268e86f4"), null, "Patient", "Patient" }
                });
        }
    }
}
