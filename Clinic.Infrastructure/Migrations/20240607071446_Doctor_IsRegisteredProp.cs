using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Doctor_IsRegisteredProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7ce30e2e-ec10-4b79-8e8b-5c445c39991e"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a70b5a19-5279-465c-a8a9-f4924d485897"));

            migrationBuilder.AddColumn<bool>(
                name: "IsRegistered",
                table: "Doctors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("198c0e8f-f26a-48a5-9163-0c1712782c6b"), null, "Doctor", "Doctor" },
                    { new Guid("599318aa-8bab-4761-a759-d0edd96c34c2"), null, "Patient", "Patient" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("198c0e8f-f26a-48a5-9163-0c1712782c6b"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("599318aa-8bab-4761-a759-d0edd96c34c2"));

            migrationBuilder.DropColumn(
                name: "IsRegistered",
                table: "Doctors");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("7ce30e2e-ec10-4b79-8e8b-5c445c39991e"), null, "Doctor", "Doctor" },
                    { new Guid("a70b5a19-5279-465c-a8a9-f4924d485897"), null, "Patient", "Patient" }
                });
        }
    }
}
