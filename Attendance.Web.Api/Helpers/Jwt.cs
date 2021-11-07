using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Attendance.Web.Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace Attendance.Web.Api.Helpers
{
    /// <summary>
        /// Security helper class.
        /// </summary>
        public static class Jwt
        {
            /// <summary>
            /// Generate JWT Token.
            /// </summary>
            /// <param name="userInfo">Object with user details.</param>
            /// <param name="userId">user Identifier</param>
            /// <param name="config">WebConfig with API details.</param>
            /// <returns>JSON WEB Token.</returns>
            public static Task<string> GenerateJsonWebToken(int userId, IConfiguration config)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim("UserId", userId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                int.TryParse(config["Jwt:TokenLifeSpan"], out var tokenLifeSpan);

                var token = new JwtSecurityToken(
                    config["Jwt:Issuer"],
                    config["Jwt:Issuer"],
                    claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.Now.AddMinutes(tokenLifeSpan),
                    signingCredentials: credentials);

                return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
            }
        }
}
