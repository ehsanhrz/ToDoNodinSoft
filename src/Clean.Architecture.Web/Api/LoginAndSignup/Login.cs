using Clean.Architecture.Core.Interfaces;
using Clean.Architecture.Core.UserAggregate.Interfaces;
using Clean.Architecture.Web.DTOs;
using Microsoft.AspNetCore.Mvc;
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
            var checkResult = await _userLogin.CheckUserNameAndPassWordLogin(dto.username, dto.password);

            if (checkResult.IsSuccess)
            {
                var token = _tokenProvider.Generate(checkResult.Value);
                var response = new
                {
                    userDetails = checkResult.Value,
                    token = token.Value
                };
                return Ok(response);
            }
            else
            {
                return BadRequest(checkResult.Errors);
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return Problem(title: "Internal server error", statusCode: 500);
        }

    }
}
