using ForestOfTasks.Api.Contracts.Users;
using ForestOfTasks.Application.Users.Commands.CreateUser;
using ForestOfTasks.Application.Users.Queries.LoginUser;
using ForestOfTasks.Application.Users.Queries.UserDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ForestOfTasks.Api.Controllers.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController(ISender mediator) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await mediator.Send(
          new CreateUserCommand(request.Email, request.Username, request.Password));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Value);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await mediator.Send(
          new LoginUserQuery(request.Email, request.Password));

        if (!result.IsSuccess)
        {
            return Unauthorized(result.Errors);
        }

        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Detail(Guid id)
    {
        var result = await mediator.Send(new UserDetailQuery(id));

        if (!result.IsSuccess)
        {
            return NotFound(result.Errors);
        }

        return Ok(result.Value);
    }
}
