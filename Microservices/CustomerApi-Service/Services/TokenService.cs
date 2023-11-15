using Consul;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsersApi.Data;

namespace UsersApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        private readonly IConfiguration _config;

        private readonly UserManager<UserEntity> _userManager;

        public TokenService(IConfiguration config, UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:Key"]));
        }

        public async Task<string> GenerateToken(UserEntity User)
        {
            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.Email,User.Email),
                new Claim("Username",User.UserName)
            };




            var UserEntity = await _userManager.FindByEmailAsync(User.Email);

            var ROLES = await _userManager.GetRolesAsync(UserEntity);

            if (ROLES != null && ROLES.Count > 0)
            {
                ROLES.ToList().ForEach(role => {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                });
            }


            var credencials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenConfiguration = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credencials,
                Issuer = _config["JWTSettings:Issuer"],
                Audience = _config["JWTSettings:Audience"]

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfiguration);

            return tokenHandler.WriteToken(token);

        }

        public async Task<bool> ValidateToken(string Token)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = _config["JWTSettings:Issuer"],
                    ValidAudience = _config["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:Key"]))
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }



}
