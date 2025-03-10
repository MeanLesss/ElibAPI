using ElibAPI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ElibAPI.Controllers.Global_Function
{
    public class Global
    {
        IConfiguration _configuration;

        public Global()
        {
            
        }
        public Global(IConfiguration configuration) {
            _configuration = configuration;
        }
        public string GenerateJWTToken(User user)
        { 
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                new Claim("Id", Guid.NewGuid().ToString("n")),
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString("n"))
             }),
                Expires = DateTime.Now.AddMinutes(15),
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
            return stringToken;
             
        }
        public bool ValidateJWTToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            try
            { 
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,  // This will validate the "exp" claim automatically
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                // This will throw an exception if the token is invalid (either expired or tampered)
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                // If we get here, the token is valid
                return true;
            }
            catch (SecurityTokenExpiredException)
            {
                // The token has expired
                Console.WriteLine("Token has expired.");
                return false;
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                // The token has an invalid signature (it was tampered with)
                Console.WriteLine("Token has an invalid signature.");
                return false;
            }
            catch (Exception ex)
            {
                // Other validation errors (e.g., token is invalid)
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return false;
            }
        }
        public string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes).ToLower(); // .NET 5 +

            }
        }
    }
}
