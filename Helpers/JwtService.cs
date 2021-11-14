using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Tracker.Helpers
{
    public class JwtService
    {
        private string secureKey = "this is a very secure key";
        private double expDate = 1440; //expiration in minutes ( one day)

        public JwtService(IConfiguration config)
        {
            secureKey = config.GetSection("JwtConfig").GetSection("secret").Value;
            expDate = double.Parse(config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value);
        }
        public string Generate(int id, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secureKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.NameIdentifier, id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(expDate),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);


            //var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));

            //var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //var header = new JwtHeader(credentials);

            //var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1));

            //var securityToken = new JwtSecurityToken(header, payload);

            //return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var securityTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(secureKey);

            securityTokenHandler.ValidateToken(jwt, new TokenValidationParameters { 
                                                        IssuerSigningKey=new SymmetricSecurityKey(key),
                                                        ValidateIssuerSigningKey = true,
                                                        ValidateIssuer = false,
                                                        ValidateAudience = false
                                                    },
                                                out SecurityToken validatedToken);

            return ( JwtSecurityToken) validatedToken;
        }

        public int GetUserId(HttpRequest request)
        {
            string authHeader = request.Headers["Authorization"];
            // authHeader = authHeader.TrimStart("Bearer ");
            var token = Verify(authHeader.Substring("Bearer ".Length));

            var idClaim = token.Claims
                .FirstOrDefault(x => x.Type == "nameid");

            return int.Parse(idClaim.Value);

        }

    }
}
