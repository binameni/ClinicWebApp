using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Doctor_keyChanged_UsernameRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Doctors_DoctorInfoUsername",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors");

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
                name: "Username",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "DoctorInfoUsername",
                table: "AspNetUsers",
                newName: "DoctorInfoDoctorNumber");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_DoctorInfoUsername",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_DoctorInfoDoctorNumber");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorNumber",
                table: "Doctors",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors",
                column: "DoctorNumber");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("005b4419-1fe8-4a5a-bf88-e02360365776"), null, "Doctor", "Doctor" },
                    { new Guid("c033d941-3826-4829-a0d7-fad959be2d0a"), null, "Patient", "Patient" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Doctors_DoctorInfoDoctorNumber",
                table: "AspNetUsers",
                column: "DoctorInfoDoctorNumber",
                principalTable: "Doctors",
                principalColumn: "DoctorNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Doctors_DoctorInfoDoctorNumber",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors");

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

            migrationBuilder.RenameColumn(
                name: "DoctorInfoDoctorNumber",
                table: "AspNetUsers",
                newName: "DoctorInfoUsername");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_DoctorInfoDoctorNumber",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_DoctorInfoUsername");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorNumber",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
                    { new Guid("198c0e8f-f26a-48a5-9163-0c1712782c6b"), null, "Doctor", "Doctor" },
                    { new Guid("599318aa-8bab-4761-a759-d0edd96c34c2"), null, "Patient", "Patient" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Doctors_DoctorInfoUsername",
                table: "AspNetUsers",
                column: "DoctorInfoUsername",
                principalTable: "Doctors",
                principalColumn: "Username");
        }
    }
}
