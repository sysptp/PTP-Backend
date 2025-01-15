using DataLayer.Models.ModuloCampaña;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EntitiesConfiguration.ModuloCampaña
{
    public class CmpServidoresSmtpConfiguration : IEntityTypeConfiguration<CmpServidoresSmtp>
    {
        public void Configure(EntityTypeBuilder<CmpServidoresSmtp> builder)
        {
            builder.ToTable("CmpServidoresSmtp");
            builder.HasKey(X => X.ServidorId);

        }
    }
}
