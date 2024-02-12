using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internshub.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class AddFullNameAndAddressFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4a930840-ebf6-4d4f-adf1-87aa7965d4a8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a13698a6-16e0-4e6e-9e01-535d29872488", "AQAAAAIAAYagAAAAEAg5FaZSJ/inxeL6cfdYdZX5i2rLCsii9bUj3bO3vyZXYTGeGZ++Zztn5dPRzTwIqQ==", "87405417-afae-4cb4-ae34-91eed20cf590" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4a930840-ebf6-4d4f-adf1-87aa7965d4a8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d13677cf-7ade-49c3-89d1-0ede30ea681f", "AQAAAAIAAYagAAAAEMxvBgByjmjq8DTxezvTYbMz1YY123By00nc7v35tqz/JJugRXtNLqayeb0qW9rXvA==", "bd2e843b-c610-47a9-8c69-e139a1e3747c" });
        }
    }
}
