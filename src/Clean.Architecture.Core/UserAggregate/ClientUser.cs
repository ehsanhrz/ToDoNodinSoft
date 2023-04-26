using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.SharedKernel.Interfaces;
using Clean.Architecture.SharedKernel;

namespace Clean.Architecture.Core.UserAggregate;
public class ClientUser : EntityBase, IAggregateRoot
{

  public ClientUser(string UserName, string PassWord, string PhoneNumber)
  {
    this.UserName = UserName;
    this.Password = PassWord;
    this.PhoneNumber = PhoneNumber;
  }

  public ClientUser(string UserName, string PassWord)
  {
    this.UserName= UserName;
    this.Password= PassWord;
    this.UserVerfied= false;
    this.EmailVerfied= false;
    this.PhoneNumberVerfied= false;
  }

  public bool UserVerfied { get; set; }

  public string Name { get; set; } = string.Empty;

  public string Password { get; set; } = string.Empty;

  public string Email { get; set; } = string.Empty;

  public string UserName { get; set; } = string.Empty;

  public bool EmailVerfied { get; set; }

  public bool TwoStepLogin { get; set; }

  public bool PhoneNumberVerfied { get; set; }

  public string PhoneNumber { get; set; } = string.Empty;
}
