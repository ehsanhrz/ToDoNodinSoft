using System.Text;
using Clean.Architecture.Core.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Clean.Architecture.Web;

public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
{
  private IOptions<JwtOptions> _options; 
  public JwtBearerOptionsSetup(IOptions<JwtOptions> options)
  {

    _options = options;

  }

  public void Configure(JwtBearerOptions options)
  {
    options.TokenValidationParameters = new()
    {
      ValidateAudience = true,
      ValidateIssuer = true,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,
      ValidIssuer = _options.Value.Issuer,
      ValidAudience = _options.Value.Aduincerr,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.SecretKey))
    };
  }
}
