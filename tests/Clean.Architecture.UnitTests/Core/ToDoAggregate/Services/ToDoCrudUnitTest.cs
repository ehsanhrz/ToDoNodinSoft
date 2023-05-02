using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.ToDoAggregate;
using Clean.Architecture.Core.ToDoAggregate.Services;
using Clean.Architecture.Core.UserAggregate;
using Clean.Architecture.SharedKernel.Interfaces;
using Moq;
using Xunit;

namespace Clean.Architecture.UnitTests.Core.ToDoAggregate.Services;
public class ToDoCrudUnitTest
{
  private Mock<IRepository<ToDo>> _mockRepository;
  private Mock<IRepository<ClientUser>> _mockRepositoryClientUser;
  private ToDoCRUD _toDoCRUD;

  private readonly string _testUserName = "EhsanHrz";
  private readonly string _testPassWord = "987987987";
  private readonly string _testPhoneNumber = "09141178787";


  private ClientUser CreateClientUser()
  {
    return new ClientUser(_testUserName, _testPassWord, _testPhoneNumber);
  }

  public ToDoCrudUnitTest()
  {
    _mockRepository = new Mock<IRepository<ToDo>>();
    _mockRepositoryClientUser = new Mock<IRepository<ClientUser>>();
    _toDoCRUD = new ToDoCRUD(_mockRepository.Object);
  }

  [Fact]
  public async Task CreateUserToDoProperly()
  {
    // first we create a test user to mock the entity ownership
    var TestUser = CreateClientUser();

    // after that we create a test User We call the CreateUserToDos service to check unit.
    var TestTodo = new ToDo(TestUser.Id, "Test", "Test");
    var ListTestTodo = new List<ToDo>
    {
      TestTodo
    };

    var result2 = await _toDoCRUD.CreateUserToDos(ListTestTodo);

    Assert.NotNull(result2);
    Assert.Equal(Ardalis.Result.ResultStatus.Ok, result2.Status);

  }

  [Fact]
  public async Task CompleteUserToDo()
  {
    // first we create a test user to mock the entity ownership
    var TestUser = CreateClientUser();

    // after that we create a test User We call the CreateUserToDos service to check unit.
    var TestTodo = new ToDo(TestUser.Id, "Test", "Test");
    var ListTestTodo = new List<ToDo>
    {
      TestTodo
    };

    foreach (ToDo toDo in ListTestTodo)
    {
      toDo.CompleteTheTask();
    }

    var result2 = await _toDoCRUD.CompleteUserToDos(ListTestTodo);


    Assert.NotNull(result2);
    Assert.Equal(Ardalis.Result.ResultStatus.Ok, result2.Status);
  }

  [Fact]
  public async Task DeleteUserTodoO()
  {
    // first we create a test user to mock the entity ownership
    var TestUser = CreateClientUser();

    // after that we create a test User We call the CreateUserToDos service to check unit.
    var TestTodo = new ToDo(TestUser.Id, "Test", "Test");
    var ListTestTodo = new List<ToDo>
    {
      TestTodo
    };

    var result2 = await _toDoCRUD.DeleteUserToDos(ListTestTodo);


    Assert.NotNull(result2);
    Assert.Equal(Ardalis.Result.ResultStatus.Ok, result2.Status);
  }

  [Fact]
  public async Task UpdateUserToDos()
  {
    // first we create a test user to mock the entity ownership
    var TestUser = CreateClientUser();

    // after that we create a test User We call the CreateUserToDos service to check unit.
    var TestTodo = new ToDo(TestUser.Id, "Test", "Test");
    var ListTestTodo = new List<ToDo>
    {
      TestTodo
    };

    var result2 = await _toDoCRUD.UpdateUserToDos(ListTestTodo);


    Assert.NotNull(result2);
    Assert.Equal(Ardalis.Result.ResultStatus.Ok, result2.Status);
  }

  
}
