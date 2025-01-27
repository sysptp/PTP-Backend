using DataLayer.Models.ModuloCampaña;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration.ModuloCampaña
{
    public class CmpClienteConfiguration : IEntityTypeConfiguration<CmpCliente>
    {
        public void Configure(EntityTypeBuilder<CmpCliente> builder)
        {
            builder.ToTable("CmpClientes");
            builder.HasKey(x => x.ClientId);

            
        }
    }
}
