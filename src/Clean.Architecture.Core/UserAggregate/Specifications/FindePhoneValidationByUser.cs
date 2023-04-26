using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Clean.Architecture.Core.UserAggregate.Specifications;
public class FindePhoneValidationByUser : Specification<PhoneValidation>
{
  public FindePhoneValidationByUser(Guid UserId, string PhoneNumber)
  {
    Query.Where(item => item.UserID == UserId && item.UserPhoneNumber == PhoneNumber);
  }

}
