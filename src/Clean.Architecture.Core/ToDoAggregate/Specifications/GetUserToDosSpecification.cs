using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Clean.Architecture.Core.ToDoAggregate.Specifications;
public class GetUserToDosSpecification : Specification<ToDo>
{
  public GetUserToDosSpecification(Guid? UserId)
  {

    if (UserId != null)
    {
      Query.Where(item => item.UserId == UserId);
    }
    else
    {
      //Query.Where(item => item.Id != null);
      Query.IgnoreQueryFilters();
    }
        
  }
}
