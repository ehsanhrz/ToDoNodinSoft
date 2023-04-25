using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Clean.Architecture.Core.UserAggregate.Specifications;
public class CheckUserNameUnique : Specification<ClientUser>
{
  public CheckUserNameUnique(string UserName)
  {
    Query.Where(item => item.UserName == UserName);
  }

}
