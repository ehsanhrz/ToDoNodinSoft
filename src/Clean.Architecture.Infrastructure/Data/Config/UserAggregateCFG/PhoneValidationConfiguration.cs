using Clean.Architecture.Core.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Architecture.Infrastructure.Data.Config.UserAggregateCFG;
public class PhoneValidationConfiguration : IEntityTypeConfiguration<PhoneValidation>
{
    public void Configure(EntityTypeBuilder<PhoneValidation> builder)
    {
        builder.ToTable("PhoneValidations");

        builder.HasKey(p => p.Id);

        builder.HasOne<ClientUser>(x => x.user)
          .WithMany(x => x.PhoneCodes)
          .HasForeignKey(x => x.UserID)
          .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.UserPhoneNumber)
          .HasMaxLength(12)
          .IsRequired();

        builder.Property(p => p.Code);
    }
}
