using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClientSupplierApi.Services
{
    public class TokenService
    {
        public static string GenerateToken(IConfiguration configuration, string userName, string email)
        {
            var tokenHandle = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["jwt:key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Subjet
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Email, email)
                }),

                Expires = DateTime.UtcNow.AddMinutes(30),

                SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandle.CreateToken(tokenDescriptor);

            return tokenHandle.WriteToken(token);
        }
    }
}
