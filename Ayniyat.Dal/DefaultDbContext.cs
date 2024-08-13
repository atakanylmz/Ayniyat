using Ayniyat.Dal.Configurations;
using Ayniyat.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayniyat.Dal
{
    public class DefaultDbContext:DbContext
    {
        public DefaultDbContext():base()
        {
            
        }
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("ayniyat");


            modelBuilder.ApplyConfiguration(new DaireConfiguration());
            modelBuilder.ApplyConfiguration(new KullaniciConfiguration());
            modelBuilder.ApplyConfiguration(new RolConfiguration());
            modelBuilder.ApplyConfiguration(new SubeConfiguration());
            modelBuilder.ApplyConfiguration(new ZimmetConfiguration());
            modelBuilder.ApplyConfiguration(new ZimmetLogConfiguration());

            SeedData(modelBuilder);
        }

        public DbSet<Daire> Daireler { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Rol> Roller { get; set; }
        public DbSet<Sube> Subeler { get; set; }
        public DbSet<Zimmet> Zimmetler { get; set; }
        public DbSet<ZimmetLog> ZimmetLoglari { get; set; }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Daire>().HasData(
                new Daire {Id=1, Ad = "Bilgi İşlem Dairesi Başkanlığı" }
                /*
                 ,new Daire { Ad = "... Dairesi Başkanlığı" }
                 ,new Daire { Ad = "...  Dairesi Başkanlığı" }
                 */
                );
            modelBuilder.Entity<Sube>().HasData(
                new Sube {Id=1, Ad = "Yazılım Geliştirme Şubesi Müdürlüğü",DaireId=1 }
                , new Sube { Id = 2, Ad = "​​​​​​​​​​​​​​​​​​​​​​​Ağ ve Sistem Yönetimi Şubesi Müdürlüğü", DaireId = 1 }
                , new Sube { Id = 3, Ad = "Coğrafi Bilgi Teknolojileri Şubesi Müdürlüğü", DaireId = 1 }
                );
            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id = 1, Ad = "SisYon", Aciklama = "Sistemdeki özellikleri belirler" }
                , new Rol { Id = 2, Ad = "Admin", Aciklama = "Ayniyatçı" }
                , new Rol { Id = 3, Ad = "Personel", Aciklama = "Üzerine zimmet yapılan kişi" }
                , new Rol { Id = 4, Ad = "OrtakAlan", Aciklama = "Kişiye zimmetlenemeyen durumlar" }
                );
            modelBuilder.Entity<Kullanici>().HasData(
                new Kullanici 
                {
                    Id=1,
                    Ad="Atakan",
                    Soyad="YILMAZ",
                    RolId=1,
                    Eposta="atakan.yilmaz@kgm.gov.tr",
                    SubeId=1,
                    Unvan= "UYGULAMA GELİŞTİRME TEKNİK ELEMANI",
                    Aktifmi=true
                },
                new Kullanici
                {
                    Id = 2,
                    Ad = "Uğur",
                    Soyad = "AFŞAR",
                    RolId = 2,
                    Eposta = "uafsar@kgm.gov.tr",
                    SubeId = 1,
                    Unvan = "TAŞINIR KAYIT KONTROL YETKİLİSİ",
                    Aktifmi = true
                },
                new Kullanici
                {
                    Id = 3,
                    Ad = "CBS",
                    Soyad = "TOPLANTI SALONU",
                    RolId = 4,
                    Eposta = null,
                    SubeId = 3,
                    Unvan = "TOPLANTI SALONU",
                    Aktifmi = true
                },
                new Kullanici
                {
                    Id = 4,
                    Ad = "Ağ Sist.",
                    Soyad = "TOPLANTI SALONU",
                    RolId = 4,
                    Eposta = null,
                    SubeId = 3,
                    Unvan = "TOPLANTI SALONU",
                    Aktifmi = true
                }
                );
        }

    }
}
