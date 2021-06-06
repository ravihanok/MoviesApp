using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.JwtToken
{
    public class JwtTokenProvider : IJwtTokenProvider
    {
        private readonly IConfiguration _cfg;
        public JwtTokenProvider(IConfiguration configuration)
        {
            _cfg = configuration;
        }
        public string GenerateToken(IdentityUser user, IList<string> roles, IList<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Token:key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor {
                Issuer = _cfg["Token:Issuer"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials,

            });
            return tokenHandler.WriteToken(token);
        }
    }
}
