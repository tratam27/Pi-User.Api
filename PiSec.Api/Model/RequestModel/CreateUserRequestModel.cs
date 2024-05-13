using System.ComponentModel.DataAnnotations;

namespace PiSec.Api.Model.RequestModel
{
    public class CreateUserRequestModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
