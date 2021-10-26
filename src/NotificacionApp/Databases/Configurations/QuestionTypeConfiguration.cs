using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificacionApp.Domain;

namespace NotificacionApp.Database.Configurations
{
    public class QuestionTypeConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Room)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.Table)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.CreateDateTime)
                .IsRequired();

            builder.Property(p => p.ResponseDateTime);


            builder.Property(p => p.Topic)
                .HasMaxLength(1000)
                .IsRequired();

            builder.HasOne(p => p.Student);
            builder.HasOne(p => p.Teacher);
        }
    }
}
