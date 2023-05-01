using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.SharedKernel.Interfaces;
using Clean.Architecture.SharedKernel;
using System.Text.Json.Serialization;
using Clean.Architecture.Core.ToDoAggregate;

namespace Clean.Architecture.Core.UserAggregate;
public class ClientUser : EntityBase, IAggregateRoot
{

  [JsonConstructor]
  public ClientUser() { }
  public ClientUser(string UserName, string Password, string PhoneNumber)
  {
    this.UserName = UserName;
    this.Password = Password;
    this.PhoneNumber = PhoneNumber;
    this.UserVerfied = false;
    this.EmailVerfied = false;
    this.PhoneNumberVerfied = false;
  }

  public ClientUser(string UserName, string Password)
  {
    this.UserName= UserName;
    this.Password= Password;
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

  [JsonIgnore]
  public ICollection<PhoneValidation> PhoneCodes { get; set; } = new List<PhoneValidation>();

  [JsonIgnore]
  public ICollection<ToDo> toDos { get; set; } = new List<ToDo>();
}
