using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompletedTaskCouner", "ConfirmPassword", "DateOfBirth", "Email", "EmployerId", "Name", "Password", "PhoneNumber", "Salary" },
                values: new object[,]
                {
                    { new Guid("b71ca60c-073b-4579-8068-3bd32a3b20ba"), 0, "employee", new DateTime(2021, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "employee1@gmail.com", new Guid("739f25f0-70a2-45ea-94a2-4ae4711669d4"), "Test Employee 2", "employee", "0879899248", 123m },
                    { new Guid("dc4f1f74-2dc7-4f38-aac9-435036a86036"), 0, "employee", new DateTime(2020, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "employee@gmail.com", new Guid("21f06ca6-348e-40b8-8fa1-ef5f2866ecb5"), "Test Employee", "employee", "0879899248", 123m }
                });

            migrationBuilder.InsertData(
                table: "Employers",
                columns: new[] { "Id", "ConfirmPassword", "DateOfBirth", "Email", "Name", "Password", "PhoneNumber", "Salary" },
                values: new object[,]
                {
                    { new Guid("21f06ca6-348e-40b8-8fa1-ef5f2866ecb5"), "admin", new DateTime(2023, 3, 10, 1, 4, 36, 809, DateTimeKind.Local).AddTicks(8055), "admin@gmail.com", "Test Admin", "admin", "0879899248", 1234m },
                    { new Guid("739f25f0-70a2-45ea-94a2-4ae4711669d4"), "admin", new DateTime(2023, 3, 10, 1, 4, 36, 809, DateTimeKind.Local).AddTicks(8118), "admin1@gmail.com", "Test Admin 2", "admin", "0879899248", 12345m }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "AssigneeId", "Description", "DueDate", "IsDone", "Title" },
                values: new object[] { new Guid("60c77ad4-db0b-4406-acde-9e0d0eac9921"), new Guid("dc4f1f74-2dc7-4f38-aac9-435036a86036"), "LONG TEXT up to 150", new DateTime(2023, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Example Test Task" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("b71ca60c-073b-4579-8068-3bd32a3b20ba"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("dc4f1f74-2dc7-4f38-aac9-435036a86036"));

            migrationBuilder.DeleteData(
                table: "Employers",
                keyColumn: "Id",
                keyValue: new Guid("21f06ca6-348e-40b8-8fa1-ef5f2866ecb5"));

            migrationBuilder.DeleteData(
                table: "Employers",
                keyColumn: "Id",
                keyValue: new Guid("739f25f0-70a2-45ea-94a2-4ae4711669d4"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("60c77ad4-db0b-4406-acde-9e0d0eac9921"));
        }
    }
}
