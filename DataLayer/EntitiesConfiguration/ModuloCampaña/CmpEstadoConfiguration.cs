using DataLayer.Models.ModuloCampaña;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration.ModuloCampaña
{
    public class CmpEstadoConfiguration : IEntityTypeConfiguration<CmpEstado>
    {
        public void Configure(EntityTypeBuilder<CmpEstado> builder)
        {
            try
            {
                builder.ToTable("CmpEstado");
                builder.HasKey(x=> x.EstadoId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
