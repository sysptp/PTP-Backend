using DataLayer.Models.ModuloCampaña;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration.ModuloCampaña
{
    public class CmpTipoPlantillaConfiguration : IEntityTypeConfiguration<CmpTipoPlantilla>
    {
        public void Configure(EntityTypeBuilder<CmpTipoPlantilla> builder)
        {
            builder.ToTable("CmpTipoPlantilla");

            builder.HasKey(x => x.Id);
        }
    }
}
