using Microsoft.Build.Framework;

namespace Clean.Architecture.Web.DTOs;

public class LoginDTO
{
  [Required]
  public string username { get; set; } = string.Empty;

  [Required]
  public string password { get; set; } = string.Empty;
}
