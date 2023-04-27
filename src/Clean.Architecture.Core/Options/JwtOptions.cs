using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Options;
public class JwtOptions
{
  public string Issuer { get; set; } = string.Empty;

  public string Aduincerr { get; set; } = string.Empty;

  public string SecretKey { get; set; } = string.Empty;
}
