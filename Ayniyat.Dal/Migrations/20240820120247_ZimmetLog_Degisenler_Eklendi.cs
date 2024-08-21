using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ayniyat.Dal.Migrations
{
    /// <inheritdoc />
    public partial class ZimmetLog_Degisenler_Eklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Degisenler",
                schema: "ayniyat",
                table: "ZimmetLoglari",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Degisenler",
                schema: "ayniyat",
                table: "ZimmetLoglari");
        }
    }
}
