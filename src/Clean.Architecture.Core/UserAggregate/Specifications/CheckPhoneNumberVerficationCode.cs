using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Clean.Architecture.Core.UserAggregate.Specifications;
public class CheckPhoneNumberVerficationCode : Specification<PhoneValidation>
{

  public CheckPhoneNumberVerficationCode(string PhoneNumber, Guid Id, int UserInput)
  {
    Query.Where(item => item.UserPhoneNumber == PhoneNumber && item.UserID == Id && item.Code == UserInput);
  }

}
