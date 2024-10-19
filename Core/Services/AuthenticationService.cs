
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    internal class AuthenticationService(UserManager<User> userManager, IOptions<JwtOptions> options) : IAuthenticationService
    {
        public async Task<UserResultDTO> LoginAsync(LoginDTO loginModel)
        {

            // Check if Email Exists 
            var user = await userManager.FindByEmailAsync(loginModel.Email);
            if (user == null) throw new UnAuthorizedException("Email Doesn't Exist");
            // Check Password 
            var result = await userManager.CheckPasswordAsync(user, loginModel.Password);
            if (!result) throw new UnAuthorizedException();

            return new UserResultDTO(
                user.DisplayName,
                user.Email!,
                await CreateTokenAsync(user));

        }

        public async Task<UserResultDTO> RegisterAsync(RegisterDTO registerModel)
        {

            var user = new User
            {
                Email = registerModel.Email,
                DisplayName = registerModel.DisplayName,
                PhoneNumber = registerModel.PhoneNumber,
                UserName = registerModel.UserName,
            };

            var result = await userManager.CreateAsync(user, registerModel.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);

            }

            return new UserResultDTO(
                user.DisplayName,
                user.Email!,
                await CreateTokenAsync(user));

        }

        private async Task<string> CreateTokenAsync(User user)
        {
            var jwtOptions = options.Value;

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name , user.UserName!),
                new Claim(ClaimTypes.Email , user.Email!)
            };

            var roles = await userManager.GetRolesAsync(user);


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            foreach (var role in roles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));


            var token = new JwtSecurityToken(
                claims: authClaims,
                issuer: jwtOptions.Issure,
                audience: jwtOptions.Audience,
                expires: DateTime.UtcNow.AddDays(jwtOptions.DurationInDays),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
