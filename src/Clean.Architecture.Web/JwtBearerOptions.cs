using System.Text;
using Clean.Architecture.Core.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Clean.Architecture.Web;

public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
{
  private IOptions<JwtOptions> _options; 
  public JwtBearerOptionsSetup(IOptions<JwtOptions> options)
  {

    _options = options;

  }

  public void Configure(string? name, JwtBearerOptions options)
  {
    options.TokenValidationParameters = new TokenValidationParameters()
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
  public void Configure(JwtBearerOptions options)
  {
    Configure(Options.DefaultName, options);
  }
}
