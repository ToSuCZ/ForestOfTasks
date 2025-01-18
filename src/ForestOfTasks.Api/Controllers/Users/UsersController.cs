using ForestOfTasks.Api.Contracts.Users;
using ForestOfTasks.Application.Users.Commands.CreateUser;
using ForestOfTasks.Application.Users.Queries.LoginUser;
using ForestOfTasks.Application.Users.Queries.UserDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ForestOfTasks.Api.Controllers.Users;

[Route("api/[controller]/[action]")]
internal class UsersController(ISender mediator) : ApiControllerBase
{
  [HttpPost]
  public async Task<IActionResult> RegisterAsync(RegisterRequest request)
  {
    var result = await mediator.Send(
      new CreateUserCommand(request.Email, request.Username, request.Password));
    
    if (!result.IsSuccess)
    {
      return BadRequest(result.Errors);
    }
    
    return Ok($"Registered as: {result.Value.UserName} with1 {result.Value.Email}");
  }
  
  [HttpPost]
  public async Task<IActionResult> LoginAsync(LoginRequest request)
  {
    var result = await mediator.Send(
      new LoginUserQuery(request.Email, request.Password));
    
    if (!result.IsSuccess)
    {
      return Unauthorized();
    }
    
    return Ok($"Token: {result.Value}");
  }
  
  [HttpGet]
  public async Task<IActionResult> DetailAsync(UserDetailRequest request)
  {
    var result = await mediator.Send(
      new UserDetailQuery(request.UserId));

    if (!result.IsSuccess)
    {
      return NotFound();
    }
    
    return Ok(result.Value);
  }
}
