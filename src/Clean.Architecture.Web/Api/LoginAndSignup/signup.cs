using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Clean.Architecture.Core.UserAggregate.Interfaces;
using Clean.Architecture.Web.DTOs;
using Clean.Architecture.Core.UserAggregate;
using Clean.Architecture.Core.Interfaces;

namespace Clean.Architecture.Web.Api.LoginAndSignup;
[Route("api/[controller]")]
[ApiController]
public class Signup : ControllerBase
{
    private IUserSignup _signupService;
    private ILogger<Signup> _logger;
    private ITokenProvider _tokenProvider;
    public Signup(IUserSignup signupService, ILogger<Signup> logger, ITokenProvider tokenProvider)
    {
        _signupService = signupService;
        _logger = logger;
        _tokenProvider = tokenProvider;
    }

    [HttpPost("getInformation", Name = "getInformation")]
    public async Task<IActionResult> GetInformation([FromBody] SignupDTO dto)
    {
        try
        {
            var UsernameCheck = await _signupService.CheckUserNameUnique(dto.username);
            var PhoneNumberCheck = await _signupService.CheckPhoneNumberUnique(dto.phonenumber);

            if (UsernameCheck.IsSuccess && PhoneNumberCheck.IsSuccess)
            {
                var NewUser = await _signupService.CreateUser(dto.username, dto.password, dto.phonenumber);

                await _signupService.FireVerficationCodeSenderEvent(NewUser.Value);

                return Ok(NewUser.Value);
            }
            else
            {
                var response = new
                {
                    UsernameCheck = UsernameCheck.Errors,
                    PhoneNumberCheck = PhoneNumberCheck.Errors,
                };

                return BadRequest(response);
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return Problem(title: "Internal server error", statusCode: 500);
        }

    }

    // this function after validating user will return jwt token

    [HttpPost("validateUserByPhoneNumber", Name = "validateUserByPhoneNumber")]
    public async Task<IActionResult> validateUserByPhoneNumber([FromBody] validateUserByPhoneNumberDTO dto)
    {
        try
        {
            var ValidateUser = await _signupService.CheckPhoneNumberVerficationCode(dto.user, dto.code);
            if (ValidateUser.IsSuccess)
            {
                var JwtToken = _tokenProvider.Generate(dto.user);
                var reesponse = new
                {
                    token = JwtToken.Value,
                };
                return Ok(reesponse);

            }
            else
            {
                return BadRequest(ValidateUser.Errors);
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return Problem(title: "Internal server error", statusCode: 500);
        }
    }

}
