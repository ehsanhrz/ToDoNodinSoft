using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Clean.Architecture.Core.UserAggregate.Specifications;
public class CheckPhoneNumberUnique : Specification<ClientUser>
{
  public CheckPhoneNumberUnique(string PhoneNumber)
  {
    Query.Where(item => item.PhoneNumber == PhoneNumber);
  }

}
