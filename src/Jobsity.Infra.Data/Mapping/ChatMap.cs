using Jobsity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobsity.Infra.Data.Mapping
{
    public class ChatMap : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            // Primary Key.
            builder.HasKey(c => c.Id);

            // Properties.
            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.Property(c => c.UpdatedAt);

            builder.Property(c => c.Name)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(i => i.Name)
                .IsUnique();

            builder.HasMany<User>(m => m.Users)
                .WithMany(x => x.Chats);

            // Table & Column Mappings.
            builder.ToTable("Chats");
        }
    }
}
