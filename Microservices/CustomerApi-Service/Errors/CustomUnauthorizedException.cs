namespace UsersApi.Errors
{
    public class CustomUnauthorizedException : Exception
    {
        public CustomUnauthorizedException(string message) : base(message)
        {

        }
    }
}
