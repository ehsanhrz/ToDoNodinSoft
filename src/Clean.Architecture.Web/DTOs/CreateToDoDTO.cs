using Microsoft.Build.Framework;

namespace Clean.Architecture.Web.DTOs;

public class CreateToDoDTO
{
  [Required]
  public Guid userID { get; set; }

  [Required]
  public string toDoTitle { get; set; } = string.Empty;

  [Required]
  public string toDoDescription { get; set;} = string.Empty;
}
