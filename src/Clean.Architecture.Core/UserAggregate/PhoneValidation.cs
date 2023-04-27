using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.SharedKernel.Interfaces;
using Clean.Architecture.SharedKernel;
using System.Runtime.CompilerServices;
using System.Reflection.Metadata.Ecma335;

namespace Clean.Architecture.Core.UserAggregate;
public class PhoneValidation : EntityBase, IAggregateRoot
{
  public Guid UserID { get; set; }

  public int Code { get; set; }

  public DateTime ValidTime { get; set; }

  public ClientUser? user { get; set; } = null;

  public string UserPhoneNumber { get; set; }

  public PhoneValidation(Guid UserID, int Code, string userPhoneNumber)
  {
    this.UserID = UserID;
    this.Code = Code;
    this.ValidTime = DateTime.UtcNow.AddMinutes(3);
    UserPhoneNumber = userPhoneNumber;
  }

  public bool ResetCode(int code)
  {
    if(ValidTime < DateTime.UtcNow)
    {
      this.Code = code;
      this.ValidTime = DateTime.UtcNow.AddMinutes(3);
      return true;
    }
    return false;
    
  }

  public void ChangeUserPhoneNumber(string phoneNumber,int Code)
  {
    this.UserPhoneNumber = phoneNumber;
    this.ValidTime = DateTime.UtcNow.AddMinutes(3);
    this.Code = Code;
  }


}
