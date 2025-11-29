using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smart_Library_Management_System_api.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "ISBN",
                keyValue: "978-0134685991",
                column: "PublicationYear",
                value: 2018);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "ISBN",
                keyValue: "978-0134685991",
                column: "PublicationYear",
                value: 0);
        }
    }
}
