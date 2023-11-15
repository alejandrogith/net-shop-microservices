using Microsoft.AspNetCore.Mvc;
using UsersApi.Dtos;
using UsersApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UsersApi.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDto>> Register([FromBody] UserRegisterRequestDto requestDto)
        {

            var User = await _userService.Register(requestDto);

            return CreatedAtAction(nameof(Register), User);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResponseDto>> Login([FromBody] UserLoginRequestDto requestDto)
        {

            var User = await _userService.Login(requestDto);

            return Ok( User);
        }


        [HttpGet("exist/{email}")]
        public async Task<ActionResult<bool>> Exist([FromRoute] string email)
        {

            var Exist = await _userService.Exist(email);

            return Ok(Exist);
        }


        [HttpGet("validatetoken/{token}")]
        public async Task<ActionResult<bool>> ValidateToken([FromRoute] string token)
        {

            var Valid = await _tokenService.ValidateToken(token);

            return Ok(Valid);
        }

    }
}