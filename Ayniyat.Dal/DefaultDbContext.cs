using Ayniyat.Dal.Configurations;
using Ayniyat.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ayniyat.Models.Utilities.Enums;

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
                new Daire {Id=1, Ad = "Bilgi İşlem Dairesi Başkanlığı" },
                new Daire {Id=2, Ad = "Destek Hizmetleri Dairesi Başkanlığı" },
                new Daire {Id=3, Ad = "Personel Dairesi Başkanlığı" },
                new Daire {Id=4, Ad = "İç Denetim Birimi Başkanlığı" },
                new Daire {Id=5, Ad = "Strateji Geliştirme Dairesi Başkanlığı" },
                new Daire {Id=6, Ad = "Teftiş Kurulu Başkanlığı" },
                new Daire {Id=7, Ad = "Program ve İzleme Dairesi Başkanlığı" },
                new Daire {Id=8, Ad = "Taşınmazlar Dairesi Başkanlığı" },
                new Daire {Id=9, Ad = "Sanat Yapıları Dairesi Başkanlığı" },
                new Daire {Id=10, Ad = "Makine ve İkmal Dairesi Başkanlığı" },
                new Daire {Id=11, Ad = "İşletmeler Dairesi Başkanlığı" },
                new Daire {Id=12, Ad = "Trafik Güvenliği Dairesi Başkanlığı" },
                new Daire {Id=13, Ad = "Tesisler ve Bakım Dairesi Başkanlığı" },
                new Daire {Id=14, Ad = "Yol Yapım Dairesi Başkanlığı" },
                new Daire {Id=15, Ad = "Araştırma ve Geliştirme Dairesi Başkanlığı" },
                new Daire {Id=16, Ad = "Etüt, Proje ve Çevre Dairesi Başkanlığı" }
               
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
                    RolId=(int)ERole.SisYon,
                    Eposta="atakan.yilmaz@kgm.gov.tr",
                    SubeId=(int)ESube.Yazilim,
                    Unvan= "UYGULAMA GELİŞTİRME TEKNİK ELEMANI",
                    Aktifmi=true
                },
                new Kullanici
                {
                    Id = 2,
                    Ad = "Uğur",
                    Soyad = "AFŞAR",
                    RolId = (int)ERole.Admin,
                    Eposta = "uafsar@kgm.gov.tr",
                    SubeId = (int)ESube.AgSistem,
                    Unvan = "TAŞINIR KAYIT KONTROL YETKİLİSİ",
                    Aktifmi = true
                },
                new Kullanici
                {
                    Id = 3,
                    Ad = "CBS",
                    Soyad = "TOPLANTI SALONU",
                    RolId = (int)ERole.OrtakAlan,
                    Eposta = null,
                    SubeId = (int)ESube.CBS,
                    Unvan = "TOPLANTI SALONU",
                    Aktifmi = true
                },
                new Kullanici
                {
                    Id = 4,
                    Ad = "Ağ Sist.",
                    Soyad = "TOPLANTI SALONU",
                    RolId = (int)ERole.OrtakAlan,
                    Eposta = null,
                    SubeId = (int)ESube.AgSistem,
                    Unvan = "TOPLANTI SALONU",
                    Aktifmi = true
                }
                );
        }

    }
}
