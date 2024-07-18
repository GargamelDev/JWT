

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT
{
    public class AuthenticateService
    {
        public string Authenticate()
        {
            var user = new User() { Id = 1, Name = "a" };
            string token = GenerateJWT(user);
            return token;
        }

        private string GenerateJWT(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name.ToString()),
                new Claim(CustomClaimTypes.EmployeeNumber, user.EmployeeNumber.ToString())
                //new Claim(ClaimTypes.Role, user.Role.ToString()),
            };
            var secret = "asfdasfdasfasfda";
            var bytes = Encoding.UTF8.GetBytes(secret);
            var key = new SymmetricSecurityKey(bytes);
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(60);

            var jwtIssuer = "SOR";

            var token = new JwtSecurityToken(
                jwtIssuer, jwtIssuer, claims, expires: expires, signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);

        }

    }

    public class User
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int EmployeeNumber { get; set; }
    }

    public class CustomClaimTypes
    {
        public static readonly string EmployeeNumber = "EmployeeNumber";
        public static readonly string EmployeeMail = "EmployeeMail";
    }
}
