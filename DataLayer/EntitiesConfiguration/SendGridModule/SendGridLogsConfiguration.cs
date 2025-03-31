using DataLayer.Models.SendGridModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration.SendGridModule{
    public class SendGridLogsConfiguration : IEntityTypeConfiguration<SendGridLogs>
    {
        public void Configure(EntityTypeBuilder<SendGridLogs> builder)
        {
            builder.ToTable("SendGridLogs");
            builder.HasKey(x=> x.Id);
        }
    }
} 
