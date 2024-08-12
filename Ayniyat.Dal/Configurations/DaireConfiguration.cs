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
    public class DaireConfiguration : IEntityTypeConfiguration<Daire>
    {
        public void Configure(EntityTypeBuilder<Daire> builder)
        {
            builder.HasKey(x => x.Id);
            
        }
    }
}
