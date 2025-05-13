using Microsoft.IdentityModel.Tokens;
using Server.Models.Database;
using Server.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _symmetricSecurityKey;
        private readonly IProjectAndUserBindingRepository _projectAndUserBindingRepository;

        public TokenService(IConfiguration configuration, IProjectAndUserBindingRepository projectAndUserBindingRepository)
        {
            _configuration = configuration;
            _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
            _projectAndUserBindingRepository = projectAndUserBindingRepository;
        }

        public async Task<string> CreateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserID.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Name),
                new Claim("CanEditUser", user.UserID.ToString()),
            };

            if (user.Role == "Admin")
            {
                claims.Add(new Claim("Role", "Admin"));
            }

            var bindings = await _projectAndUserBindingRepository.GetProjectAndUserBindingsByUserIdAsync(user.UserID);
            foreach (var binding in bindings)
            {
                claims.Add(new Claim("CanEditProject", binding.ProjectID.ToString()));
            }

            var signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = signingCredentials,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
