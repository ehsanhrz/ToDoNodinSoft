﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;

namespace Clean.Architecture.Core.UserAggregate.Interfaces;
public interface IUserLogin
{
  public Task<Result> CheckUserNameAndPassWordLogin(string UserName, string PassWord);
  
  public Task<Result> CreateJWTToken(ClientUser user);
}
