using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace ElibAPI.Controllers;

[AllowAnonymous]
public class ApiTokenController : Controller
{
    private readonly IConfiguration configuration;

  
    [HttpPost]
    [Route("/ApiToken/GenerateToken")]
    [Produces("application/json")]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = "2.Token")]
    public Object GenerateToken(string username, string pwd)
    {
        if (username == "mean2" && pwd == "123")
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                new Claim("Id", Guid.NewGuid().ToString("n")),
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Email, username),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString("n"))
             }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);
            return new {
                GenerateToken = stringToken
            };
        }
        return Unauthorized();
    }
    public ApiTokenController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
}

