using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class patient_visit_removed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_AspNetUsers_PatientId",
                table: "Visits");

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

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Visits",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Visits_PatientId",
                table: "Visits",
                newName: "IX_Visits_ApplicationUserId");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("60367e35-088c-4f51-a804-0d1a05fa04a7"), null, "Doctor", "Doctor" },
                    { new Guid("ee7debd2-9d80-4082-be89-11f344f20549"), null, "Patient", "Patient" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_AspNetUsers_ApplicationUserId",
                table: "Visits",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_AspNetUsers_ApplicationUserId",
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

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Visits",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Visits_ApplicationUserId",
                table: "Visits",
                newName: "IX_Visits_PatientId");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("b4d57e20-bd34-487d-a74a-cce974d0811c"), null, "Patient", "Patient" },
                    { new Guid("eef23b95-6a7b-4b1b-8826-77c015df83b1"), null, "Doctor", "Doctor" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_AspNetUsers_PatientId",
                table: "Visits",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
