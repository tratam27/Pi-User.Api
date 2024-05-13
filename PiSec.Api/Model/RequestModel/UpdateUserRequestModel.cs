using System.ComponentModel.DataAnnotations;

namespace PiSec.Api.Model.RequestModel
{
    public class UpdateUserRequestModel
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }
}
