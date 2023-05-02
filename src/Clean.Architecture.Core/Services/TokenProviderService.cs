using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using Clean.Architecture.Core.Interfaces;
using Clean.Architecture.Core.Options;
using Clean.Architecture.Core.UserAggregate;
using Clean.Architecture.Core.UserAggregate.Interfaces;
using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Clean.Architecture.Core.Services;
public class TokenProviderService : ITokenProvider
{
  private readonly IOptions<JwtOptions> _options;
  public TokenProviderService(IOptions<JwtOptions> options)
  {
    _options = options;
  } 
  public Result<string> Generate(ClientUser user)
  {
    var claims = new Claim[]
    {
      new Claim("Id",user.Id.ToString()),
      new Claim(JwtRegisteredClaimNames.Name, user.UserName),
      new Claim(JwtRegisteredClaimNames.Iss, _options.Value.Issuer),
      new Claim(JwtRegisteredClaimNames.Aud, _options.Value.Aduincerr)
    };

    var signingCredentials = new SigningCredentials(
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.SecretKey)), SecurityAlgorithms.HmacSha256);

    
    var tokenHandler = new JwtSecurityTokenHandler();
   
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.UtcNow.AddDays(15),
      SigningCredentials = signingCredentials,
      
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return Result.Success(tokenHandler.WriteToken(token));
  }
}
