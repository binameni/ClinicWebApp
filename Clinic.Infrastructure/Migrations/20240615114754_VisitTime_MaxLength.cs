using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VisitTime_MaxLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("005b4419-1fe8-4a5a-bf88-e02360365776"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c033d941-3826-4829-a0d7-fad959be2d0a"));

            migrationBuilder.AlterColumn<string>(
                name: "Time",
                table: "Visits",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Time",
                table: "Visits",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("005b4419-1fe8-4a5a-bf88-e02360365776"), null, "Doctor", "Doctor" },
                    { new Guid("c033d941-3826-4829-a0d7-fad959be2d0a"), null, "Patient", "Patient" }
                });
        }
    }
}
