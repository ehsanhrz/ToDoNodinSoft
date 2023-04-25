using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;

namespace Clean.Architecture.Core.UserAggregate.Interfaces;
public interface IUserSignup
{
  public Task<Result> CheckUserNameUnique(string UserName);

  public Task<Result> CheckPhoneNumberUnique(string PhoneNumber);

  public Task<Result> FireVerficationCodeSenderEvent(ClientUser user);

  public Task<Result> ResetVerficationCodeSenderEvent(ClientUser user);

  public Task<Result> CheckPhoneNumberVerficationCode(ClientUser user, string code);

  public Task<Result> FireCreateUserEvent(ClientUser user);
  
}
