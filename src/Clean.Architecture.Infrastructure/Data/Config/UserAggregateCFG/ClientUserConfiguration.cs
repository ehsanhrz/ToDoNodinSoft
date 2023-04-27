using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.UserAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.Infrastructure.Data.Config.UserAggregateCFG;
public class ClientUserConfiguration : IEntityTypeConfiguration<ClientUser>
{
  public void Configure(EntityTypeBuilder<ClientUser> builder)
  {
    builder.ToTable(nameof(ClientUser));

    builder.HasKey(p => p.Id);

    builder.Property(p => p.UserName)
      .HasMaxLength(128)
      .IsRequired();

    builder.Property<string>(p => p.Password).IsRequired();

    builder.Property<bool>(p => p.UserVerfied).IsRequired();

    builder.Property<bool>(p => p.EmailVerfied).IsRequired();

    builder.Property<string>(p => p.Email);

    builder.Property<string>(p => p.PhoneNumber).HasMaxLength(12);

    builder.Property<bool>(p => p.PhoneNumberVerfied).HasDefaultValue(false);

    builder.Property<string>(p => p.Name).HasMaxLength(120);

    builder.Property<bool>(p => p.TwoStepLogin).HasDefaultValue(false);

  }
}
