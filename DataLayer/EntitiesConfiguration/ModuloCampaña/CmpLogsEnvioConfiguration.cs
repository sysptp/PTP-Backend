using DataLayer.Models.ModuloCampaña;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration.ModuloCampaña
{
    public class CmpLogsEnvioConfiguration : IEntityTypeConfiguration<CmpLogsEnvio>
    {
        public void Configure(EntityTypeBuilder<CmpLogsEnvio> builder)
        {
            builder.ToTable("CmpLogsEnvio");
            builder.HasKey(x => x.LogId);
        }
    }
}
