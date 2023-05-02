using Clean.Architecture.Core.ToDoAggregate;
using Clean.Architecture.Core.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Architecture.Infrastructure.Data.Config.TodoAggregateCFG;
public class ToDoConfig : IEntityTypeConfiguration<ToDo>
{

  public void Configure(EntityTypeBuilder<ToDo> builder)
  {
    builder.HasKey(p => p.Id);

    builder.HasOne<ClientUser>(x => x.User)
         .WithMany(x => x.toDos)
         .HasForeignKey(x => x.UserId)
         .OnDelete(DeleteBehavior.Restrict);
  }
}
