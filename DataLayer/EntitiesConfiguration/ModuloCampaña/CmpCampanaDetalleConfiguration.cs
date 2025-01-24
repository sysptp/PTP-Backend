using DataLayer.Models.ModuloCampaña;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration.ModuloCampaña
{
    public class CmpCampanaDetalleConfiguration : IEntityTypeConfiguration<CmpCampanaDetalle>
    {
        public void Configure(EntityTypeBuilder<CmpCampanaDetalle> builder)
        {
            builder.ToTable("CmpCampanaDetalle");
            builder.HasKey(x=> x.CampanaDetalleId);

            builder.HasOne(x=> x.CmpCliente)
                .WithMany(x=> x.CampanaDetalles)
                .HasForeignKey(x=> x.ClientId)
                .OnDelete(DeleteBehavior.Restrict); 
            
            builder.HasOne(x=> x.CmpCampana)
                .WithMany(x=> x.CampanaDetalles)
                .HasForeignKey(x=> x.CampanaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
