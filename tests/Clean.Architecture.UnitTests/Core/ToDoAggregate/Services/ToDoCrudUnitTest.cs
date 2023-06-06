using Clean.Architecture.Core.ToDoAggregate;
using Clean.Architecture.Core.ToDoAggregate.Services;
using Clean.Architecture.Core.UserAggregate;
using Clean.Architecture.SharedKernel.Interfaces;
using Moq;
using Xunit;

namespace Clean.Architecture.UnitTests.Core.ToDoAggregate.Services;
public class ToDoCrudUnitTest
{
  
  private readonly ToDoCrud _toDoCrud;

  private const string TestUserName = "EhsanHrz";
  private const string TestPassWord = "987987987";
  private const string TestPhoneNumber = "09141178787";


  private ClientUser CreateClientUser()
  {
    return new ClientUser(TestUserName, TestPassWord, TestPhoneNumber);
  }

  public ToDoCrudUnitTest()
  {
    Mock<IRepository<ToDo>> mockRepository = new();
    
    _toDoCrud = new ToDoCrud(mockRepository.Object);
  }

  [Fact]
  public async Task CreateUserToDoProperly()
  {
    // first we create a test user to mock the entity ownership
    var testUser = CreateClientUser();

    // after that we create a test User We call the CreateUserToDos service to check unit.
    var testTodo = new ToDo(testUser.Id, "Test", "Test");
    var listTestTodo = new List<ToDo>
    {
      testTodo
    };

    var result2 = await _toDoCrud.CreateUserToDos(listTestTodo);

    Assert.NotNull(result2);
    Assert.Equal(Ardalis.Result.ResultStatus.Ok, result2.Status);

  }

  [Fact]
  public async Task CompleteUserToDo()
  {
    // first we create a test user to mock the entity ownership
    var testUser = CreateClientUser();

    // after that we create a test User We call the CreateUserToDos service to check unit.
    var testTodo = new ToDo(testUser.Id, "Test", "Test");
    var listTestTodo = new List<ToDo>
    {
      testTodo
    };

    foreach (ToDo toDo in listTestTodo)
    {
      toDo.CompleteTheTask();
    }

    var result2 = await _toDoCrud.CompleteUserToDos(listTestTodo);


    Assert.NotNull(result2);
    Assert.Equal(Ardalis.Result.ResultStatus.Ok, result2.Status);
  }

  [Fact]
  public async Task DeleteUserTodoO()
  {
    // first we create a test user to mock the entity ownership
    var testUser = CreateClientUser();

    // after that we create a test User We call the CreateUserToDos service to check unit.
    var testTodo = new ToDo(testUser.Id, "Test", "Test");
    var listTestTodo = new List<ToDo>
    {
      testTodo
    };

    var result2 = await _toDoCrud.DeleteUserToDos(listTestTodo);


    Assert.NotNull(result2);
    Assert.Equal(Ardalis.Result.ResultStatus.Ok, result2.Status);
  }

  [Fact]
  public async Task UpdateUserToDos()
  {
    // first we create a test user to mock the entity ownership
    var testUser = CreateClientUser();

    // after that we create a test User We call the CreateUserToDos service to check unit.
    var testTodo = new ToDo(testUser.Id, "Test", "Test");
    var listTestTodo = new List<ToDo>
    {
      testTodo
    };

    var result2 = await _toDoCrud.UpdateUserToDos(listTestTodo);


    Assert.NotNull(result2);
    Assert.Equal(Ardalis.Result.ResultStatus.Ok, result2.Status);
  }

  
}
