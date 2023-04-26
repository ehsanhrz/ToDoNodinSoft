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

  public void VerifiUser()
  {
    this.UserVerfied = true;
    this.PhoneNumberVerfied = true;
  }
  public bool UserVerfied { get; private set; }

  public string Name { get; set; } = string.Empty;

  public string Password { get; set; } = string.Empty;

  public string Email { get; set; } = string.Empty;

  public string UserName { get; set; } = string.Empty;

  public bool EmailVerfied { get; private set; }

  public bool TwoStepLogin { get; private set; }

  public bool PhoneNumberVerfied { get; private set; }

  public string PhoneNumber { get; set; } = string.Empty;
}
