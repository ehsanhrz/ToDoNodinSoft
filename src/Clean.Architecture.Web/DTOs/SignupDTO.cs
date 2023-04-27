using System.ComponentModel.DataAnnotations;

namespace Clean.Architecture.Web.DTOs;

public class SignupDTO
{
  [Required]
  public string username { get; set; } = string.Empty;

  [Required]
  public string password { get; set; } = string.Empty;

  [Required]
  [MaxLength(12)]
  public string phonenumber { get; set; } = string.Empty;
}
