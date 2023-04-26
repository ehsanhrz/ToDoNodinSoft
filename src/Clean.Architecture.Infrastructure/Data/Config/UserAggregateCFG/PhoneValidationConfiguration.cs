using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Architecture.Infrastructure.Data.Config.UserAggregateCFG;
public class PhoneValidationConfiguration : IEntityTypeConfiguration<PhoneValidation>
{
  public void Configure(EntityTypeBuilder<PhoneValidation> builder)
  {
    builder.HasKey(p => p.Id);

    builder.Property(p => p.UserID).IsRequired();

    builder.Property(p => p.UserPhoneNumber)
      .HasMaxLength(12)
      .IsRequired();

    builder.Property(p => p.Code);
  }
}
