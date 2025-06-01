using DataLayer.Models.SendGridModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration.SendGridModule{
    public class SendGridTemplateConfiguration : IEntityTypeConfiguration<SendGridTemplate>
    {
        public void Configure(EntityTypeBuilder<SendGridTemplate> builder)
        {
            builder.ToTable("SendGridTemplate");
            builder.HasKey(x=> x.Id);
            builder.HasQueryFilter(x=> x.IsActive && !x.Borrado);
        }
    }
}