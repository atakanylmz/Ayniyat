using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ayniyat.Dal.Migrations
{
    /// <inheritdoc />
    public partial class Baslangic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ayniyat");

            migrationBuilder.CreateTable(
                name: "Daireler",
                schema: "ayniyat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Daireler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roller",
                schema: "ayniyat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: false),
                    Aciklama = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subeler",
                schema: "ayniyat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: false),
                    DaireId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subeler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subeler_Daireler_DaireId",
                        column: x => x.DaireId,
                        principalSchema: "ayniyat",
                        principalTable: "Daireler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                schema: "ayniyat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: false),
                    Soyad = table.Column<string>(type: "text", nullable: false),
                    Eposta = table.Column<string>(type: "text", nullable: true),
                    Unvan = table.Column<string>(type: "text", nullable: true),
                    Aktifmi = table.Column<bool>(type: "boolean", nullable: false),
                    RolId = table.Column<int>(type: "integer", nullable: false),
                    SubeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kullanicilar_Roller_RolId",
                        column: x => x.RolId,
                        principalSchema: "ayniyat",
                        principalTable: "Roller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kullanicilar_Subeler_SubeId",
                        column: x => x.SubeId,
                        principalSchema: "ayniyat",
                        principalTable: "Subeler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Zimmetler",
                schema: "ayniyat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StokNo = table.Column<int>(type: "integer", nullable: true),
                    TasinirNo = table.Column<string>(type: "text", nullable: true),
                    MalzemeAd = table.Column<string>(type: "text", nullable: false),
                    EnvanterNo = table.Column<string>(type: "text", nullable: true),
                    Birim = table.Column<string>(type: "text", nullable: true),
                    Miktar = table.Column<int>(type: "integer", nullable: false),
                    SeriNo = table.Column<string>(type: "text", nullable: true),
                    Model = table.Column<string>(type: "text", nullable: true),
                    KayitTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    KaldirilmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Aciklama = table.Column<string>(type: "text", nullable: true),
                    SubeId = table.Column<int>(type: "integer", nullable: false),
                    KullaniciId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zimmetler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zimmetler_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalSchema: "ayniyat",
                        principalTable: "Kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zimmetler_Subeler_SubeId",
                        column: x => x.SubeId,
                        principalSchema: "ayniyat",
                        principalTable: "Subeler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZimmetLoglari",
                schema: "ayniyat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StokNo = table.Column<int>(type: "integer", nullable: true),
                    TasinirNo = table.Column<string>(type: "text", nullable: true),
                    MalzemeAd = table.Column<string>(type: "text", nullable: false),
                    EnvanterNo = table.Column<string>(type: "text", nullable: true),
                    Birim = table.Column<string>(type: "text", nullable: true),
                    Miktar = table.Column<int>(type: "integer", nullable: false),
                    SeriNo = table.Column<string>(type: "text", nullable: true),
                    Model = table.Column<string>(type: "text", nullable: true),
                    IslemTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Aciklama = table.Column<string>(type: "text", nullable: true),
                    ZimmetId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZimmetLoglari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZimmetLoglari_Zimmetler_ZimmetId",
                        column: x => x.ZimmetId,
                        principalSchema: "ayniyat",
                        principalTable: "Zimmetler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "ayniyat",
                table: "Daireler",
                columns: new[] { "Id", "Ad" },
                values: new object[] { 1, "Bilgi İşlem Dairesi Başkanlığı" });

            migrationBuilder.InsertData(
                schema: "ayniyat",
                table: "Roller",
                columns: new[] { "Id", "Aciklama", "Ad" },
                values: new object[,]
                {
                    { 1, "Sistemdeki özellikleri belirler", "SisYon" },
                    { 2, "Ayniyatçı", "Admin" },
                    { 3, "Üzerine zimmet yapılan kişi", "Personel" },
                    { 4, "Kişiye zimmetlenemeyen durumlar", "OrtakAlan" }
                });

            migrationBuilder.InsertData(
                schema: "ayniyat",
                table: "Subeler",
                columns: new[] { "Id", "Ad", "DaireId" },
                values: new object[,]
                {
                    { 1, "Yazılım Geliştirme Şubesi Müdürlüğü", 1 },
                    { 2, "​​​​​​​​​​​​​​​​​​​​​​​Ağ ve Sistem Yönetimi Şubesi Müdürlüğü", 1 },
                    { 3, "Coğrafi Bilgi Teknolojileri Şubesi Müdürlüğü", 1 }
                });

            migrationBuilder.InsertData(
                schema: "ayniyat",
                table: "Kullanicilar",
                columns: new[] { "Id", "Ad", "Aktifmi", "Eposta", "RolId", "Soyad", "SubeId", "Unvan" },
                values: new object[,]
                {
                    { 1, "Atakan", true, "atakan.yilmaz@kgm.gov.tr", 1, "YILMAZ", 1, "UYGULAMA GELİŞTİRME TEKNİK ELEMANI" },
                    { 2, "Uğur", true, "uafsar@kgm.gov.tr", 2, "AFŞAR", 1, "TAŞINIR KAYIT KONTROL YETKİLİSİ" },
                    { 3, "CBS", true, null, 4, "TOPLANTI SALONU", 3, "TOPLANTI SALONU" },
                    { 4, "Ağ Sist.", true, null, 4, "TOPLANTI SALONU", 3, "TOPLANTI SALONU" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicilar_RolId",
                schema: "ayniyat",
                table: "Kullanicilar",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicilar_SubeId",
                schema: "ayniyat",
                table: "Kullanicilar",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_Subeler_DaireId",
                schema: "ayniyat",
                table: "Subeler",
                column: "DaireId");

            migrationBuilder.CreateIndex(
                name: "IX_Zimmetler_KullaniciId",
                schema: "ayniyat",
                table: "Zimmetler",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Zimmetler_SubeId",
                schema: "ayniyat",
                table: "Zimmetler",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_ZimmetLoglari_ZimmetId",
                schema: "ayniyat",
                table: "ZimmetLoglari",
                column: "ZimmetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZimmetLoglari",
                schema: "ayniyat");

            migrationBuilder.DropTable(
                name: "Zimmetler",
                schema: "ayniyat");

            migrationBuilder.DropTable(
                name: "Kullanicilar",
                schema: "ayniyat");

            migrationBuilder.DropTable(
                name: "Roller",
                schema: "ayniyat");

            migrationBuilder.DropTable(
                name: "Subeler",
                schema: "ayniyat");

            migrationBuilder.DropTable(
                name: "Daireler",
                schema: "ayniyat");
        }
    }
}
