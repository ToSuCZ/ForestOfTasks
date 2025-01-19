using System.Net;
using System.Net.Http.Json;
using ForestOfTasks.Application.Users.Contracts;
using ForestOfTasks.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace ForestOfTasks.Tests.E2E.Users;

public class UsersControllerE2ETests(
    CustomWebApplicationFactory factory
) : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly HttpClient _client = factory.CreateClient();
    private readonly ApplicationDbContext _dbContext = factory.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

    [Fact]
    public async Task RegisterUser_ShouldReturnOk_WhenRequestIsValid()
    {
        // Arrange
        var registerRequest = new
        {
            Username = "username",
            Email = "email@email.com",
            Password = "Password!12345"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/users/register", registerRequest);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<UserDto>();

        content!.Username.ShouldBe(registerRequest.Username);
        content.Email.ShouldBe(registerRequest.Email);
    }

    [Fact]
    public async Task LoginUser_ShouldReturnToken_WhenLoginIsSuccessful()
    {
        // Arrange
        var registerRequest = new
        {
            Username = "username",
            Email = "email@email.com",
            Password = "Password!12345"
        };

        await _client.PostAsJsonAsync("/api/users/register", registerRequest);

        var loginRequest = new
        {
            Email = "email@email.com",
            Password = "Password!12345"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/users/login", loginRequest);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.ShouldNotBeEmpty();
        content.ShouldBeOfType<string>();
    }

    [Fact]
    public async Task GetUserDetails_ShouldReturnOk_WhenUserExists()
    {
        // Arrange
        var registerRequest = new
        {
            Username = "username",
            Email = "email@email.com",
            Password = "Password!12345"
        };

        var registerResponse = await _client.PostAsJsonAsync("/api/users/register", registerRequest);
        var registerContent = await registerResponse.Content.ReadFromJsonAsync<UserDto>();

        // Act
        var response = await _client.GetAsync(new Uri($"/api/users/{registerContent!.Id.ToString()}"));

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<UserDto>();

        content!.Username.ShouldBe(registerRequest.Username);
        content.Email.ShouldBe(registerRequest.Email);
    }
    
    public async Task InitializeAsync()
    {
        await _dbContext.Database.EnsureCreatedAsync().ConfigureAwait(false);
    }
    
    public async Task DisposeAsync()
    {
        await _dbContext.Database.EnsureDeletedAsync().ConfigureAwait(false);
    }
}
