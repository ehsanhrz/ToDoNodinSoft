using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using Clean.Architecture.Core.UserAggregate.Interfaces;
using Clean.Architecture.Core.UserAggregate.Specifications;
using Clean.Architecture.SharedKernel.Interfaces;

namespace Clean.Architecture.Core.UserAggregate.Services;
public class UserLoginService : IUserLogin
{
  private IRepository<ClientUser> _repository;
  public UserLoginService(IRepository<ClientUser> repository)
  {
    _repository = repository;
  }
  public async Task<Result<ClientUser>> CheckUserNameAndPassWordLogin(string userName, string passWord)
  {
    var checkSpec = new CheckUserNameAndPassWordLogin(userName, passWord);

    var result = await _repository.FirstOrDefaultAsync(checkSpec);

    if (result == null)
    {
      return Result.Error("username or password is incorrect");
    }
    else
    {
      return Result.Success(result);
    }
  }
}
