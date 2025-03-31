using DataLayer.Models.SendGridModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration.SendGridModule{
    public class SendGridEConfiguration : IEntityTypeConfiguration<SendGridConfiguration>
    {
        public void Configure(EntityTypeBuilder<SendGridConfiguration> builder)
        {
            builder.ToTable("SendGridConfiguration");
            builder.HasKey(x=> x.Id);
            builder.HasQueryFilter(x=> x.IsActive && !x.Borrado);
        }
    }
}