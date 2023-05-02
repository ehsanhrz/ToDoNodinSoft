using Clean.Architecture.Core.ToDoAggregate;
using Clean.Architecture.Core.ToDoAggregate.Interfaces;
using Clean.Architecture.Web.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace Clean.Architecture.Web.Api.ToDoCrud;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ToDoCrud : ControllerBase
{
  private IToDoCRUD _toDoCRUD;
  private ILogger<ToDoCrud> _logger;
  public ToDoCrud(IToDoCRUD toDoCRUD, ILogger<ToDoCrud> logger)
  {
    _toDoCRUD = toDoCRUD;
    _logger = logger;

  }

  [AllowAnonymous]
  [HttpGet("GetUserToDos", Name = "GetUserToDos")]
  public async Task<IActionResult> GetUserToDos([FromQuery] Guid userId)
  {
    try
    {
      var result = await _toDoCRUD.GetUserToDos(userId);
      if (result.Value.Count == 0)
      {
        return NotFound("user dont have any ToDos");
      }
      else
      {
        return Ok(result.Value);
      }
    }
    catch (Exception ex)
    {
      _logger.LogInformation(ex.Message);
      return Problem(title: "Internal server error", statusCode: 500);
    }
  }

  [HttpPost("CreateUserToDos", Name = "CreateUserToDos")]
  public async Task<IActionResult> CreateUserToDos([FromBody] List<CreateToDoDTO> dTOs)
  {
    try
    {
      var ToDos = new List<ToDo>();

      foreach (var dto in dTOs)
      {
        ToDo toDo = new ToDo(dto.userID, dto.toDoTitle, dto.toDoDescription);
        ToDos.Add(toDo);
      }

      var result = await _toDoCRUD.CreateUserToDos(ToDos);
      if (result.IsSuccess)
      {
        return Ok(result.Value);
      }
      else
      {
        return BadRequest(result.Errors);
      }
    }
    catch (Exception ex)
    {
      _logger.LogInformation(ex.Message);
      return Problem(title: "Internal server error", statusCode: 500);
    }
  }

  [HttpDelete("DeleteUserToDos", Name = "DeleteUserToDos")]
  public async Task<IActionResult> DeleteUserToDos([FromBody] List<ToDo> toDos)
  {
    try
    {
      var result = await _toDoCRUD.DeleteUserToDos(toDos);
      if (result.IsSuccess)
      {
        return Ok(result.Value);
      }
      else
      {
        return BadRequest(result.Errors);
      }
    }
    catch (Exception ex)
    {
      _logger.LogInformation(ex.Message);
      return Problem(title: "Internal server error", statusCode: 500);
    }
  }

  [HttpPut("CompleteUserToDos", Name = "CompleteUserToDos")]
  public async Task<IActionResult> CompleteUserToDos([FromBody] List<ToDo> toDos)
  {
    try
    {
      var result = await _toDoCRUD.CompleteUserToDos(toDos);
      if (result.IsSuccess)
      {
        return Ok(result.Value);
      }
      else
      {
        return BadRequest(result.Errors);
      }
    }
    catch (Exception ex)
    {
      _logger.LogInformation(ex.Message);
      return Problem(title: "Internal server error", statusCode: 500);
    }
  }

  [HttpPut("UpdateUserToDos", Name = "UpdateUserToDos")]
  public async Task<IActionResult> UpdateUserToDos([FromBody] List<ToDo> toDos)
  {
    try
    {
      var result = await _toDoCRUD.UpdateUserToDos(toDos);
      if (result.IsSuccess)
      {
        return Ok(result.Value);
      }
      else
      {
        return BadRequest(result.Errors);
      }
    }
    catch (Exception ex)
    {
      _logger.LogInformation(ex.Message);
      return Problem(title: "Internal server error", statusCode: 500);
    }
  }
}
