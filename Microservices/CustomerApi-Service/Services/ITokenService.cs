using UsersApi.Data;

namespace UsersApi.Services
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(UserEntity usuario);

        public Task<bool> ValidateToken(string Token);

    }
}
