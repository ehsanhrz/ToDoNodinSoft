using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using Clean.Architecture.Core.ToDoAggregate.Records;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Clean.Architecture.Core.ToDoAggregate.Interfaces;
public interface IToDoCRUD
{
  public Task<Result<ICollection<ToDo>>> GetUserToDos(Guid? userId);

  public Task<Result> CompleteUserToDos(IEnumerable<ToDo> DTOs);

  public Task<Result> DeleteUserToDos(IEnumerable<ToDo> dtos);

  public Task<Result> CreateUserToDos(IEnumerable<ToDo> dataTransferObjects);

  public Task<Result> UpdateUserToDos(IEnumerable<ToDo> dtos);
}
