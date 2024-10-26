using Shared;
using Shared.OrderModels;

namespace Services.Abstractions
{
    public interface IAuthenticationService
    {
        // login => User Result  (LoginDto)

        // 

        public Task<UserResultDTO> LoginAsync(LoginDTO loginModel);
        public Task<UserResultDTO> RegisterAsync(RegisterDTO registerModel);

        //Get Current User 
        public Task<UserResultDTO> GetUserByEmail(string email);
        //check Email Exist
        public Task<bool> CheckEmailExist(string email);

        //Get User Address
        public Task<AddressDTO> GetUserAddress(string email);

        //Update User Address
        public Task<AddressDTO> UpdateUserAddress(AddressDTO address,string email);
    }
}
