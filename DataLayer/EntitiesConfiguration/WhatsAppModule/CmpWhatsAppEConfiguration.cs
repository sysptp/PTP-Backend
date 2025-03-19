using DataLayer.Models.Otros;
using DataLayer.Models.WhatsAppFeature;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration.WhatsAppModule
{
    public class CmpWhatsAppEConfiguration : IEntityTypeConfiguration<CmpWhatsAppConfiguration>
    {
        public void Configure(EntityTypeBuilder<CmpWhatsAppConfiguration> builder)
        {
            builder.ToTable("CmpWhatsAppConfiguration");

            builder.HasKey(e => e.ConfigurationId);
        }
    }
}