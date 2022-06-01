using Jobsity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.Infra.Data.Mapping
{
    public class MessageMap : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            // Primary Key.
            builder.HasKey(c => c.Id);

            // Properties.
            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.Property(c => c.UpdatedAt);

            builder.Property(c => c.Text)
                .IsRequired();

            builder.HasOne<Chat>(o => o.Chat)
                .WithMany(o => o.Messages)
                .HasForeignKey(o => o.ChatId);

            builder.HasOne<User>(o => o.User)
                .WithMany(o => o.Messages)
                .HasForeignKey(o => o.UserId);

            // Table & Column Mappings.
            builder.ToTable("Messages");
        }
    }
}
