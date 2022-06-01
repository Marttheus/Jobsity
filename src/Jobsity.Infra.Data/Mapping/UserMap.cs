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
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary Key.
            builder.HasKey(c => c.Id);

            builder.HasMany<Chat>(m => m.Chats)
                .WithMany(x => x.Users);

            builder.HasMany<Message>(m => m.Messages)
                .WithOne(o => o.User)
                .HasForeignKey(f => f.UserId);
        }
    }
}
