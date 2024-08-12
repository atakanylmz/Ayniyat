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
    public class ZimmetConfiguration : IEntityTypeConfiguration<Zimmet>
    {
        public void Configure(EntityTypeBuilder<Zimmet> builder)
        {
            builder.HasKey(x=>x.Id);

            builder.HasOne(x=>x.Sube)
                .WithMany(x=>x.Zimmetler)
                .HasForeignKey(x=>x.SubeId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(x=>x.Kullanici)
                .WithMany(x=>x.Zimmetler)
                .HasForeignKey(x=>x.KullaniciId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
