using JwtAuthentication.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthentication.Utility
{
    public class GenerateJWT
    {
        private readonly IOptions<ConnectionStringModel> ConnectionString;
        public GenerateJWT(IOptions<ConnectionStringModel> app)
        {
            ConnectionString = app;
        }
        public string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConnectionString.Value.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim("Password", userInfo.Password),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())};
            var token = new JwtSecurityToken(ConnectionString.Value.Issuer,
              ConnectionString.Value.Issuer,
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
