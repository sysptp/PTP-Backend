using DataLayer.Models.MessagingModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration.MessagingModule
{
    public class MessagingEConfiguration : IEntityTypeConfiguration<MessagingConfiguration>
    {
        public void Configure(EntityTypeBuilder<MessagingConfiguration> builder)
        {
            builder.ToTable("MessagingConfiguration");

            builder.HasKey(e => e.ConfigurationId);
        }
    }
}