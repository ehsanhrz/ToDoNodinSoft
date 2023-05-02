using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using Clean.Architecture.Core.ToDoAggregate.Interfaces;
using Clean.Architecture.Core.ToDoAggregate.Records;
using Clean.Architecture.Core.ToDoAggregate.Specifications;
using Clean.Architecture.SharedKernel.Interfaces;

namespace Clean.Architecture.Core.ToDoAggregate.Services;
public class ToDoCRUD : IToDoCRUD
{
  private IRepository<ToDo> _repository;
  public ToDoCRUD(IRepository<ToDo> repository)
  {
    _repository = repository;
  }

  public async Task<Result> CompleteUserToDos(IEnumerable<ToDo> dtos)
  {
    try
    {
      foreach (ToDo toDo in dtos)
      {
        toDo.CompleteTheTask();
      }

      await _repository.UpdateRangeAsync(dtos, new CancellationToken());
      await _repository.SaveChangesAsync(new CancellationToken());

      return Result.Success();

    }
    catch (Exception ex)
    {
      return Result.Error(ex.Message);
    }
    
  }

  public async Task<Result> CreateUserToDos(IEnumerable<ToDo> dtos)
  {
    try
    {
      await _repository.AddRangeAsync(dtos, new CancellationToken());
      await _repository.SaveChangesAsync(new CancellationToken());
      return Result.Success();
    }
    catch (Exception ex)
    {
      return Result.Error(ex.Message);
    }
  }

  public async Task<Result> DeleteUserToDos(IEnumerable<ToDo> dtos)
  {
    try
    {
      await _repository.DeleteRangeAsync(dtos, new CancellationToken());
      await _repository.SaveChangesAsync(new CancellationToken());
      return Result.Success();
    }
    catch (Exception ex)
    {
      return Result.Error(ex.Message);
    }
  }

  public async Task<Result<ICollection<ToDo>>> GetUserToDos(Guid userId)
  {
    var UserToDos = new GetUserToDosSpecification(userId);
    var UserToDosResult = await _repository.ListAsync(UserToDos);
    return UserToDosResult;
  }

  public async Task<Result> UpdateUserToDos(IEnumerable<ToDo> dtos)
  {
    try
    {

      await _repository.UpdateRangeAsync(dtos, new CancellationToken());
      await _repository.SaveChangesAsync(new CancellationToken());

      return Result.Success();

    }
    catch (Exception ex)
    {
      return Result.Error(ex.Message);
    }
  }
}
