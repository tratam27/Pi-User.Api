namespace PiSec.Api.Model.RequestModel
{
    public class LoginRequestModel
    {

        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
