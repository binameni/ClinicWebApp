using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newProps_Visit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("060a9564-b760-4ae4-ae3f-3d226f4eaf32"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c6e27a98-16c0-4e73-91f8-6d8b893ca138"));

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "Visits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Visits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Visits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Visits");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("060a9564-b760-4ae4-ae3f-3d226f4eaf32"), null, "Patient", "Patient" },
                    { new Guid("c6e27a98-16c0-4e73-91f8-6d8b893ca138"), null, "Doctor", "Doctor" }
                });
        }
    }
}
