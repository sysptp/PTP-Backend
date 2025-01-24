using DataLayer.Models.ModuloCampaña;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration.ModuloCampaña
{
    public class CmpAgendarCampanaConfiguration : IEntityTypeConfiguration<CmpAgendarCampana>
    {
        public void Configure(EntityTypeBuilder<CmpAgendarCampana> builder)
        {
            builder.ToTable("CmpAgendarCampana");
            builder.HasKey(x => x.ProgramacionId);

            builder.HasOne(x=> x.CmpCampana)
                .WithMany(x=> x.CmpAgendarCampanas)
                .HasForeignKey(x=> x.CampanaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x=> x.CmpFrecuencia)
                .WithMany(x=> x.CmpAgendarCampanas)
                .HasForeignKey(x=> x.FrecuenciaId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
