using DataLayer.Models.ModuloCampaña;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration.ModuloCampaña
{
    public class CmpConfiguracionesSmtpConfiguration : IEntityTypeConfiguration<CmpConfiguracionesSmtp>
    {
        public void Configure(EntityTypeBuilder<CmpConfiguracionesSmtp> builder)
        {
            builder.ToTable("CmpConfiguracionesSmtp");
            builder.HasKey(x => x.ConfiguracionId);

            builder.HasOne(x => x.ServidoresSmtp)
                .WithMany(x => x.CmpConfiguracionesSmtps)
                .HasForeignKey(x => x.ServidorId);

        }
    }
}
