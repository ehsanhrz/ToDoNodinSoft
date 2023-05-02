using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.ToDoAggregate;
using Clean.Architecture.Core.ToDoAggregate.Specifications;
using Microsoft.Extensions.Configuration.UserSecrets;
using Xunit;

namespace Clean.Architecture.UnitTests.Core.ToDoAggregate.Specifications;
public class GetUserToDos
{
  [Fact]
  public void FilterCollectionToOnlyReturnItemsWithIsDoneFalse()
  {
    var userId1 = Guid.NewGuid();
    var userId2 = Guid.NewGuid();
    var item1 = new ToDo(userId1, "test", "test");
    var item2 = new ToDo(userId1, "test", "test");
    var item3 = new ToDo(userId2, "test", "test");
    

    var items = new List<ToDo>() { item1, item2, item3 };

    var spec = new GetUserToDosSpecification(userId1);

    var filteredList = spec.Evaluate(items);

    Assert.Contains(item1, filteredList);
    Assert.Contains(item2, filteredList);
    Assert.DoesNotContain(item3, filteredList);
  }
}
