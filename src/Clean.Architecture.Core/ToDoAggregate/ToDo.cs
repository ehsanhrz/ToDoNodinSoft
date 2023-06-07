using System.Text.Json.Serialization;
using Clean.Architecture.Core.UserAggregate;
using Clean.Architecture.SharedKernel.Interfaces;
using Clean.Architecture.SharedKernel;

namespace Clean.Architecture.Core.ToDoAggregate;
public class ToDo : EntityBase, IAggregateRoot
{
  [JsonConstructor]
  public ToDo()
  {
    
  }

  public ToDo(Guid userId, string toDoTitle, string? toDoDescription)
  {
    this.UserId = userId;
    this.ToDoTitle = toDoTitle;
    this.ToDoDescription = toDoDescription ?? "";
    IsCompleted = false;
  }
  public Guid UserId { get; set; }

  public string ToDoTitle { get;  set; } = string.Empty;

  public string ToDoDescription { get; set;} = string.Empty;

  public bool IsCompleted { get; set; }

  [JsonIgnore]
  public ClientUser? User { get; set; } = null;

  public void CompleteTheTask()
  {
    IsCompleted = true;
  }
}
