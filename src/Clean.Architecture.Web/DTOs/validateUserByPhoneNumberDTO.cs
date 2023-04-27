using System.ComponentModel.DataAnnotations;
using Clean.Architecture.Core.UserAggregate;

namespace Clean.Architecture.Web.DTOs;

public class validateUserByPhoneNumberDTO
{
  [Required]
  public ClientUser user { get; set; } = new(string.Empty, string.Empty);

  [Required]
  public int code { get; set; }
}
