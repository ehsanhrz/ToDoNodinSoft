using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Clean.Architecture.Core.UserAggregate.Specifications;
public class CheckUniqueEmail : Specification<ClientUser>
{
  public CheckUniqueEmail(string Email)
  {
    Query.Where(item => item.Email == Email);
  }
}
