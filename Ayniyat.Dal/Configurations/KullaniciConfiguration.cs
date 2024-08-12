using Ayniyat.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayniyat.Dal.Configurations
{
    public class KullaniciConfiguration : IEntityTypeConfiguration<Kullanici>
    {
        public void Configure(EntityTypeBuilder<Kullanici> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x=>x.Rol)
                .WithMany(x=>x.Kullanicilar)
                .HasForeignKey(x=>x.RolId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Sube)
                .WithMany(x => x.Kullanicilar)
                .HasForeignKey(x => x.SubeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
