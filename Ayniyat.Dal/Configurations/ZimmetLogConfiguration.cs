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
    public class ZimmetLogConfiguration : IEntityTypeConfiguration<ZimmetLog>
    {
        public void Configure(EntityTypeBuilder<ZimmetLog> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x=>x.Zimmet)
                .WithMany(x=>x.ZimmetLoglari)
                .HasForeignKey(x=>x.ZimmetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
