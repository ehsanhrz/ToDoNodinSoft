using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.SharedKernel;

namespace Clean.Architecture.Core.UserAggregate;
public class Emailvalidation : EntityBase
{
  public Guid UserId { get; set; }

  public string Email { get; set; } = string.Empty;

  public string HashCode { get; set; } = string.Empty;

  public DateTime ValidTime { get; set; }

  public Emailvalidation(Guid UserId, string Email, string HashCode)
  {
    this.UserId = UserId;
    this.Email = Email;
    this.HashCode = HashCode;
    this.ValidTime = DateTime.UtcNow.AddMinutes(15);
  }
}
