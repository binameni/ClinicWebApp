using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class doctorKey_addedToDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Doctor_DoctorInfoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DoctorInfoId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctor",
                table: "Doctor");

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

            migrationBuilder.DropColumn(
                name: "DoctorInfoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Doctor");

            migrationBuilder.RenameTable(
                name: "Doctor",
                newName: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "DoctorInfoUsername",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Doctors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors",
                column: "Username");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("7ce30e2e-ec10-4b79-8e8b-5c445c39991e"), null, "Doctor", "Doctor" },
                    { new Guid("a70b5a19-5279-465c-a8a9-f4924d485897"), null, "Patient", "Patient" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DoctorInfoUsername",
                table: "AspNetUsers",
                column: "DoctorInfoUsername");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Doctors_DoctorInfoUsername",
                table: "AspNetUsers",
                column: "DoctorInfoUsername",
                principalTable: "Doctors",
                principalColumn: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Doctors_DoctorInfoUsername",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DoctorInfoUsername",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors");

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

            migrationBuilder.DropColumn(
                name: "DoctorInfoUsername",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Doctors");

            migrationBuilder.RenameTable(
                name: "Doctors",
                newName: "Doctor");

            migrationBuilder.AddColumn<int>(
                name: "DoctorInfoId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Doctor",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctor",
                table: "Doctor",
                column: "Id");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("a45c2889-499c-4d3f-a708-9ba5dbcdcd57"), null, "Patient", "Patient" },
                    { new Guid("ae464d76-af51-465d-922e-7c4a6d1b9e2c"), null, "Doctor", "Doctor" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DoctorInfoId",
                table: "AspNetUsers",
                column: "DoctorInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Doctor_DoctorInfoId",
                table: "AspNetUsers",
                column: "DoctorInfoId",
                principalTable: "Doctor",
                principalColumn: "Id");
        }
    }
}
