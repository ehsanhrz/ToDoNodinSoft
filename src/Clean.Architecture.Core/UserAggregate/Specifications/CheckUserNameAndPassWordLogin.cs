using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Clean.Architecture.Core.UserAggregate.Specifications;
public class CheckUserNameAndPassWordLogin : Specification<ClientUser>
{
  public CheckUserNameAndPassWordLogin(string UserName, string PassWord)
  {
    Query.Where(item => item.UserName == UserName && item.Password == PassWord);
  }
}
