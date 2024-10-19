using Shared;

namespace Services.Abstractions
{
    public interface IAuthenticationService
    {
        // login => User Result  (LoginDto)

        // 

        public Task<UserResultDTO> LoginAsync(LoginDTO loginModel);
        public Task<UserResultDTO> RegisterAsync(RegisterDTO registerModel);
    }
}
