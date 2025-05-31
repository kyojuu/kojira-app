using System.Security.Claims;
using System.Text;
using kojira.Application.Abstractions.Authentication;
using kojira.Domain.Members;
using kojira.Domain.Users;
using kojira.Domain.Workspaces;
using kojira.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace kojira.Infrastructure.Authentication;

internal sealed class TokenProvider(IConfiguration configuration, ApplicationDbContext context) : ITokenProvider
{
    public async Task<string> Create(User user, Workspace workspace)
    {
        string secretKey = configuration["Jwt:Secret"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        List<string> members = await context.Members
            .Where(m => m.UserId == user.Id && m.WorkspaceId == workspace.Id)
            .Select(m => m.Role.Name)
            .ToListAsync();

        List<Claim> claims = 
        [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, workspace.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
        ];

        claims.AddRange(members.Select(r => new Claim(ClaimTypes.Name, r)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        var handler = new JsonWebTokenHandler();

        string token = handler.CreateToken(tokenDescriptor);

        return token;
    }
}
