using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using Clean.Architecture.Core.UserAggregate;

namespace Clean.Architecture.Core.Interfaces;
public interface ITokenProvider
{
  public Result<string> Generate(ClientUser user);
}
