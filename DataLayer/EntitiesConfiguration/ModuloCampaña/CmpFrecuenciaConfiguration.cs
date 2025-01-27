using DataLayer.Models.ModuloCampaña;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration.ModuloCampaña
{
    public class CmpFrecuenciaConfiguration : IEntityTypeConfiguration<CmpFrecuencia>
    {
        public void Configure(EntityTypeBuilder<CmpFrecuencia> builder)
        {
            builder.ToTable("CmpFrecuencia");
            builder.HasKey(x=> x.Id);
        }
    }
}
