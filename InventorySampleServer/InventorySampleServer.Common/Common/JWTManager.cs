using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Common.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ZafarTC.Common.Common
{
    public class JWTManager
    {
        private IConfiguration _Configuration;
        public JWTManager(IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }

        public string Authenticate(List<UserRoleDto> UserRole)
        {
            #region Authenticate
            var ClaimList = new List<Claim>();
            if (UserRole.Any())
            {
                foreach (var Role in UserRole)
                {
                    ClaimList.Add(new Claim(ClaimTypes.Name, Role.Mobile));
                    ClaimList.Add(new Claim(ClaimTypes.Role, Role.Title));
                    ClaimList.Add(new Claim("UserId", Role.UserId.ToString()));
                }
            }

            var Handler = new JwtSecurityTokenHandler();
            var TokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["JWT:Key"]));
            var Credentials = new SigningCredentials(TokenKey, SecurityAlgorithms.HmacSha512Signature);
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(ClaimList),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = Credentials
            };
            var JwtToken = Handler.CreateToken(TokenDescriptor);
            return Handler.WriteToken(JwtToken);
            #endregion
        }
    }
}
