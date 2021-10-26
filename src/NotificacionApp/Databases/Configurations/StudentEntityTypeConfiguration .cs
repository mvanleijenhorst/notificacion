using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificacionApp.Controllers.Common;
using NotificacionApp.Domain;

namespace NotificacionApp.Database.Configurations
{
    public class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(ValidationConstants.NormalFieldLength);

            builder
                .Property(p => p.Username)
                .IsRequired()
                .HasMaxLength(ValidationConstants.NormalFieldLength);

            builder
                .Property(p => p.Password)
                .IsRequired()
                .HasMaxLength(ValidationConstants.NormalFieldLength);


            builder.HasKey(p => p.Id);
        }
    }
}
