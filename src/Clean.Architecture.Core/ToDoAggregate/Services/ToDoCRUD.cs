using Ardalis.Result;
using Clean.Architecture.Core.ToDoAggregate.Interfaces;
using Clean.Architecture.Core.ToDoAggregate.Specifications;
using Clean.Architecture.SharedKernel.Interfaces;

namespace Clean.Architecture.Core.ToDoAggregate.Services;
public class ToDoCrud : IToDoCRUD
{
  private readonly IRepository<ToDo> _repository;
  public ToDoCrud(IRepository<ToDo> repository)
  {
    _repository = repository;
  }

  public async Task<Result> CompleteUserToDos(IEnumerable<ToDo> dataTransferObjects)
  {
    try
    {
      var transferObjects = dataTransferObjects.ToList();
      foreach (ToDo toDo in transferObjects)
      {
        toDo.CompleteTheTask();
      }

      await _repository.UpdateRangeAsync(transferObjects);
      await _repository.SaveChangesAsync();

      return Result.Success();

    }
    catch (Exception ex)
    {
      return Result.Error(ex.Message);
    }
    
  }

  public async Task<Result> CreateUserToDos(IEnumerable<ToDo> dataTransferObjects)
  {
    try
    {
      await _repository.AddRangeAsync(dataTransferObjects);
      await _repository.SaveChangesAsync();
      return Result.Success();
    }
    catch (Exception ex)
    {
      return Result.Error(ex.Message);
    }
  }

  public async Task<Result> DeleteUserToDos(IEnumerable<ToDo> dataTransferObjects)
  {
    try
    {
      await _repository.DeleteRangeAsync(dataTransferObjects);
      await _repository.SaveChangesAsync();
      return Result.Success();
    }
    catch (Exception ex)
    {
      return Result.Error(ex.Message);
    }
  }

  public async Task<Result<ICollection<ToDo>>> GetUserToDos(Guid? userId)
  {
    var userToDos = new GetUserToDosSpecification(userId);
    var userToDosResult = await _repository.ListAsync(userToDos);
    return userToDosResult;
  }

  public async Task<Result> UpdateUserToDos(IEnumerable<ToDo> dataTransferObjects)
  {
    try
    {

      await _repository.UpdateRangeAsync(dataTransferObjects);
      await _repository.SaveChangesAsync();

      return Result.Success();

    }
    catch (Exception ex)
    {
      return Result.Error(ex.Message);
    }
  }
}
