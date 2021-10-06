using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rewind.Objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;



namespace Rewind.Utilities
{
    public class JwtHelper
    {
        private const double EXPIRY_DURATION_MINUTES = 30;
        private readonly IConfiguration _config;
        public static string Key { get; set; }
        public static string Issuer { get; set; }

        public JwtHelper(IConfiguration configuration)
        { 
            _config = configuration;
            var JwtConfig = _config.GetSection("JwtConfig");
            Key = JwtConfig["Key"];
            Issuer = JwtConfig["Issuer"];
        }

        public string BuildToken(User user)
        {
            var claims = new[] {
            //new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Email, user.Phone),
            new Claim(ClaimTypes.NameIdentifier,
            Guid.NewGuid().ToString())
        };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(Issuer, Issuer, claims,
                expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        public bool IsTokenValid(string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(Key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = Issuer,
                    ValidAudience = Issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
