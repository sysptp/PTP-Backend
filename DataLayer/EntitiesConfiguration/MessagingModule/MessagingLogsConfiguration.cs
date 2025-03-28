using DataLayer.Models.MessagingModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration.MessagingModule{
    public class MessagingLogsConfiguration : IEntityTypeConfiguration<MessagingLogs>
    {
        public void Configure(EntityTypeBuilder<MessagingLogs> builder)
        {
            builder.ToTable("MessagingLogs");
            builder.HasKey(e => e.Id);
        }

      
    }
}
