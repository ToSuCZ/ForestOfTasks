using FluentResults;
using ForestOfTasks.Api.Contracts.Users;
using ForestOfTasks.Api.Controllers.Users;
using ForestOfTasks.Application.Users.Commands.CreateUser;
using ForestOfTasks.Application.Users.Contracts;
using ForestOfTasks.Application.Users.Queries.LoginUser;
using ForestOfTasks.Application.Users.Queries.UserDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;

namespace ForestOfTasks.Api.Tests.Unit.Controllers.Users;

public class UsersControllerTests
{
    private readonly ISender _mediator;
    private readonly UsersController _sut;

    public UsersControllerTests()
    {
        _mediator = Substitute.For<ISender>();
        _sut = new UsersController(_mediator);
    }

    [Fact]
    public async Task Register_ShouldReturnOk_WhenUserIsCreated()
    {
        // Arrange
        var request = new RegisterRequest("username", "email@email.com", "password");
        var userGuid = Guid.NewGuid();
        var userDto = new UserDto(userGuid, "username", "email");

        _mediator
            .Send(Arg.Any<CreateUserCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result.Ok(userDto));

        // Act
        var response = await _sut.Register(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(response);
        okResult.Value.ShouldBe(userDto);
    }

    [Fact]
    public async Task Register_ShouldReturnBadRequest_WhenUserIsNotCreated()
    {
        // Arrange
        var request = new RegisterRequest("username", "email@email.com", "password");
        var errors = new[] { new Error("Invalid password") };

        _mediator
            .Send(Arg.Any<CreateUserCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result.Fail(errors));

        // Act
        var response = await _sut.Register(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
        badRequestResult.Value.ShouldBe(errors);
    }

    [Fact]
    public async Task Login_ShouldReturnOk_WhenUserIsLoggedIn()
    {
        // Arrange
        var request = new LoginRequest("email", "password");

        _mediator
            .Send(Arg.Any<LoginUserQuery>(), Arg.Any<CancellationToken>())
            .Returns(Result.Ok("token"));

        // Act
        var response = await _sut.Login(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(response);
        okResult.Value.ShouldBe("token");
    }

    [Fact]
    public async Task Login_ShouldReturnUnauthorized_WhenUserIsNotLoggedIn()
    {
        // Arrange
        var request = new LoginRequest("email", "password");
        var errors = new[] { new Error("Invalid credentials") };

        _mediator
            .Send(Arg.Any<LoginUserQuery>(), Arg.Any<CancellationToken>())
            .Returns(Result.Fail(errors));

        // Act
        var response = await _sut.Login(request);

        // Assert
        var okResult = Assert.IsType<UnauthorizedObjectResult>(response);
        okResult.Value.ShouldBe(errors);
    }

    [Fact]
    public async Task Detail_ShouldReturnOk_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userDetail = new UserDto(userId, "username", "email");

        _mediator
            .Send(Arg.Any<UserDetailQuery>(), Arg.Any<CancellationToken>())
            .Returns(Result.Ok(userDetail));

        // Act
        var response = await _sut.Detail(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(response);
        okResult.Value.ShouldBe(userDetail);
    }

    [Fact]
    public async Task Detail_ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var errors = new[] { new Error("User not found") };

        _mediator
            .Send(Arg.Any<UserDetailQuery>(), Arg.Any<CancellationToken>())
            .Returns(Result.Fail(errors));

        // Act
        var response = await _sut.Detail(userId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(response);
        notFoundResult.Value.ShouldBe(errors);
    }
}
