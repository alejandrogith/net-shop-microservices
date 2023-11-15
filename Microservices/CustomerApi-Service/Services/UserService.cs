using Mapster;
using Microsoft.AspNetCore.Identity;
using UsersApi.Data;
using UsersApi.Dtos;
using UsersApi.Errors;

namespace UsersApi.Services
{
    public class UserService : IUserService
    {

        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly ITokenService _tokenService;



        public UserService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<bool> Exist(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email);
            var UserExist = false;

            if (User is not null) UserExist = true; 

            return UserExist;
        }

        public async Task<UserResponseDto> Login(UserLoginRequestDto request)
        {
            var ApplicationUser = await _userManager.FindByEmailAsync(request.Email);

            if (ApplicationUser is null)
                throw new CustomNotFoundException($"The User with the email: {request.Email}  does not exist");


            var Result = await _signInManager.CheckPasswordSignInAsync(ApplicationUser, request.Password, false);

            if (!Result.Succeeded)
                throw new CustomUnauthorizedException($"The user credentials is invalid");

            var Token = await _tokenService.GenerateToken(ApplicationUser);



            var UserResponse = ApplicationUser.Adapt<UserResponseDto>();

            UserResponse.Token = Token;

            return UserResponse;
        }








        public async Task<UserResponseDto> Register(UserRegisterRequestDto request)
        {
            var ApplicationUser = request.Adapt<UserEntity>();

            var Result = await _userManager.CreateAsync(ApplicationUser, request.Password);

            if (!Result.Succeeded)
            {
                var Errors = Result.Errors.Select(x => x.Description).ToList();

                throw new CustomIdentityErrors(Errors);
            }

            var Token= await _tokenService.GenerateToken(ApplicationUser);

            var UserResponse = ApplicationUser.Adapt<UserResponseDto>();

            UserResponse.Token = Token;

            return UserResponse;
        }
    }
}
