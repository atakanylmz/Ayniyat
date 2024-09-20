using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ayniyat.Dal.Migrations
{
    /// <inheritdoc />
    public partial class Daireler_Eklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "ayniyat",
                table: "Daireler",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 2, "Destek Hizmetleri Dairesi Başkanlığı" },
                    { 3, "Personel Dairesi Başkanlığı" },
                    { 4, "İç Denetim Birimi Başkanlığı" },
                    { 5, "Strateji Geliştirme Dairesi Başkanlığı" },
                    { 6, "Teftiş Kurulu Başkanlığı" },
                    { 7, "Program ve İzleme Dairesi Başkanlığı" },
                    { 8, "Taşınmazlar Dairesi Başkanlığı" },
                    { 9, "Sanat Yapıları Dairesi Başkanlığı" },
                    { 10, "Makine ve İkmal Dairesi Başkanlığı" },
                    { 11, "İşletmeler Dairesi Başkanlığı" },
                    { 12, "Trafik Güvenliği Dairesi Başkanlığı" },
                    { 13, "Tesisler ve Bakım Dairesi Başkanlığı" },
                    { 14, "Yol Yapım Dairesi Başkanlığı" },
                    { 15, "Araştırma ve Geliştirme Dairesi Başkanlığı" },
                    { 16, "Etüt, Proje ve Çevre Dairesi Başkanlığı" }
                });

            migrationBuilder.UpdateData(
                schema: "ayniyat",
                table: "Kullanicilar",
                keyColumn: "Id",
                keyValue: 2,
                column: "SubeId",
                value: 2);

            migrationBuilder.UpdateData(
                schema: "ayniyat",
                table: "Kullanicilar",
                keyColumn: "Id",
                keyValue: 4,
                column: "SubeId",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ayniyat",
                table: "Daireler",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "ayniyat",
                table: "Daireler",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "ayniyat",
                table: "Daireler",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "ayniyat",
                table: "Daireler",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "ayniyat",
                table: "Daireler",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "ayniyat",
                table: "Daireler",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "ayniyat",
                table: "Daireler",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "ayniyat",
                table: "Daireler",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "ayniyat",
                table: "Daireler",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "ayniyat",
                table: "Daireler",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "ayniyat",
                table: "Daireler",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "ayniyat",
                table: "Daireler",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "ayniyat",
                table: "Daireler",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "ayniyat",
                table: "Daireler",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "ayniyat",
                table: "Daireler",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.UpdateData(
                schema: "ayniyat",
                table: "Kullanicilar",
                keyColumn: "Id",
                keyValue: 2,
                column: "SubeId",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "ayniyat",
                table: "Kullanicilar",
                keyColumn: "Id",
                keyValue: 4,
                column: "SubeId",
                value: 3);
        }
    }
}
