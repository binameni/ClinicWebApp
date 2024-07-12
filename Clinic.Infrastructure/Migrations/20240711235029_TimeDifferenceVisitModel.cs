using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TimeDifferenceVisitModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<double>(
                name: "TimeDifference",
                table: "Visits",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("b9124e74-6681-4886-acf2-0de89ee8f64a"), null, "Patient", "Patient" },
                    { new Guid("f5dbf650-5708-4642-b109-210bc4410b09"), null, "Doctor", "Doctor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b9124e74-6681-4886-acf2-0de89ee8f64a"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f5dbf650-5708-4642-b109-210bc4410b09"));

            migrationBuilder.DropColumn(
                name: "TimeDifference",
                table: "Visits");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0159d25d-4112-4843-a2e5-52aadb3ea934"), null, "Doctor", "Doctor" },
                    { new Guid("60932496-2d13-4f69-8fdd-99507aff62f2"), null, "Patient", "Patient" }
                });
        }
    }
}
