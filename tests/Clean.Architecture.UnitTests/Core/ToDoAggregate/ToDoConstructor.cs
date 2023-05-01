using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.ToDoAggregate;
using Clean.Architecture.Core.UserAggregate;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Xunit;

namespace Clean.Architecture.UnitTests.Core.ToDoAggregate;
public class ToDoConstructor
{
  private readonly string _testUserName = "EhsanHrz";
  private readonly string _testPassWord = "987987987";
  private readonly string _testPhoneNumber = "09141178787";
  private readonly string _testToDoTitle = "TestToDo";
  private readonly string _testToDoDescription = "TestDesc";
  private ClientUser? _testClientUser;
  private ToDo? _toDo;
  private ToDo CreateToDo(Guid UserID)
  {
    var toDo = new ToDo(UserID,_testToDoTitle,_testToDoDescription);
    return toDo;
  }
  private ClientUser CreateClientUser()
  {
    return new ClientUser(_testUserName, _testPassWord, _testPhoneNumber);
  }

  [Fact]
  public void InitializesToDo()
  {
    _testClientUser = CreateClientUser();

    Assert.Equal(_testUserName, _testClientUser.UserName);
    Assert.Equal(_testPassWord, _testClientUser.Password);
    Assert.Equal(_testPhoneNumber, _testClientUser.PhoneNumber);

    _toDo = CreateToDo(_testClientUser.Id);

    Assert.Equal(_testToDoTitle, _toDo.ToDoTitle);
    Assert.Equal(_testToDoDescription, _toDo.ToDoDescription);
    Assert.Equal(_testClientUser.Id, _toDo.UserId);
  }
}
