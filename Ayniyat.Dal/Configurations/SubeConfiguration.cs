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
    public class SubeConfiguration : IEntityTypeConfiguration<Sube>
    {
        public void Configure(EntityTypeBuilder<Sube> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Daire)
                .WithMany(x => x.Subeler)
                .HasForeignKey(x => x.DaireId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
