namespace OrderApi_Service.Exeptions
{
    public class CustomBadRequestException : Exception
    {

        public CustomBadRequestException(string message) : base(message)
        {


        }


    }
}