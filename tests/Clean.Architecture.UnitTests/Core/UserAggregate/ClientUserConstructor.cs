using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.UserAggregate;
using Xunit;

namespace Clean.Architecture.UnitTests.Core.UserAggregate;
public class ClientUserConstructor
{
  private readonly string _testUserName = "EhsanHrz";
  private readonly string _testPassWord = "987987987";
  private readonly string _testPhoneNumber = "09141178787";
  private ClientUser? _testClientUser;

  private ClientUser CreateClientUser()
  {
    return new ClientUser(_testUserName, _testPassWord, _testPhoneNumber);
  }

  [Fact]
  public void InitializesUser()
  {
    _testClientUser = CreateClientUser();

    Assert.Equal(_testUserName, _testClientUser.UserName);
    Assert.Equal(_testPassWord, _testClientUser.Password);
    Assert.Equal(_testPhoneNumber, _testClientUser.PhoneNumber);
  }
}
