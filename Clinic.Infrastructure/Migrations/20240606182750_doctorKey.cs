using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class doctorKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("52d1cac5-87b6-40fa-a8f8-82a101ac59ed"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e8550cec-f473-494a-bdf4-3605ef7a07c0"));

            migrationBuilder.CreateTable(
                name: "DoctorKey",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorKey");

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

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("52d1cac5-87b6-40fa-a8f8-82a101ac59ed"), null, "Doctor", "Doctor" },
                    { new Guid("e8550cec-f473-494a-bdf4-3605ef7a07c0"), null, "Patient", "Patient" }
                });
        }
    }
}
