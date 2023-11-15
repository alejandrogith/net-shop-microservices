using UsersApi.Dtos;

namespace UsersApi.Services
{
    public interface IUserService
    {
        public Task<UserResponseDto> Register(UserRegisterRequestDto request);

        public Task<UserResponseDto> Login(UserLoginRequestDto request);

        public Task<bool> Exist(string Email);

        
    }
}
