namespace UsersApi.Dtos
{
    public record UserRegisterRequestDto(string UserName, string Email,string Password);

    public record UserLoginRequestDto( string Email, string Password);


    public class UserResponseDto { 
    
        public string UserName { get; set;}
        public string Email { get; set; }
        public string Token { get; set; }

    } 


}
