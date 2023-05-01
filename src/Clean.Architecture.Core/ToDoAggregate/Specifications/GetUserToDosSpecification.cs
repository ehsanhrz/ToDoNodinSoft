using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Clean.Architecture.Core.ToDoAggregate.Specifications;
public class GetUserToDosSpecification : Specification<ToDo>
{
  public GetUserToDosSpecification(Guid UserId)
  {
    Query.Where(item => item.UserId == UserId );
  }
}
