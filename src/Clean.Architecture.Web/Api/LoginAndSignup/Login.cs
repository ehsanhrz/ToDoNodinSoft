using Clean.Architecture.Core.Interfaces;
using Clean.Architecture.Core.UserAggregate.Interfaces;
using Clean.Architecture.Web.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Clean.Architecture.Web.Api.LoginAndSignup;
[Route("api/[controller]")]
[ApiController]
public class Login : ControllerBase
{
  private ILogger<Login> _logger;
  private ITokenProvider _tokenProvider;
  private IUserLogin _userLogin;
  public Login(IUserLogin userLogin, ILogger<Login> logger, ITokenProvider tokenProvider)
  {
    _logger = logger;
    _tokenProvider = tokenProvider;
    _userLogin = userLogin;
  }

  [HttpPost("Login", Name = "Login")]
  public async Task<IActionResult> LoginUser([FromBody] LoginDTO dto)
  {
    try
    {
      var CheckResult = await _userLogin.CheckUserNameAndPassWordLogin(dto.username, dto.password);

      if (CheckResult.IsSuccess)
      {
        var Token = _tokenProvider.Generate(CheckResult.Value);
        var reesponse = new
        {
          token = Token.Value,
        };
        return Ok(reesponse);
      }
      else
      {
        return BadRequest(CheckResult.Errors);
      }
    }
    catch(Exception ex)
    {
      _logger.LogInformation(ex.Message);
      return Problem(title: "Internal server error", statusCode: 500);
    }

  }
}
