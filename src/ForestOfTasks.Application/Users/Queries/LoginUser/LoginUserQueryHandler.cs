using System.Security.Claims;
using System.Text;
using FluentResults;
using ForestOfTasks.Application.Configuration;
using ForestOfTasks.Domain.Aggregates.UserAggregate;
using ForestOfTasks.SharedKernel.Consts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace ForestOfTasks.Application.Users.Queries.LoginUser;

internal sealed class LoginUserQueryHandler(
    UserManager<ApplicationUser> userManager,
    IConfiguration configuration,
    JsonWebTokenHandler tokenHandler
) : IRequestHandler<LoginUserQuery, Result<string>>
{
  public async Task<Result<string>> Handle(
      LoginUserQuery request,
      CancellationToken cancellationToken)
  {
    var user = await userManager.FindByEmailAsync(request.Email);
    
    if (user is null)
    {
      return Result.Fail("User not found");
    }
    
    var loginSuccess = await userManager.CheckPasswordAsync(user, request.Password);
    
    if (!loginSuccess)
    {
      return Result.Fail("Invalid password");
    }
    
    // create JWT Token
    var settings = configuration.GetSection(ConfigSections.Auth).Get<JwtSettings>();

    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email!),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };
    
    var token = new SecurityTokenDescriptor{
        
        Issuer = settings!.JwtIssuer,
        Audience = settings.JwtAudience,
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddMinutes(settings.JwtDurationInMinutes),
        SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings!.JwtSecret)),
            SecurityAlgorithms.HmacSha256)
    };
    
    return Result.Ok(tokenHandler.CreateToken(token));
  }
}
