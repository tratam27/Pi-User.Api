using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PiSec.Api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public User(string name, string email)
        {
            this.Name = name;
            this.Email = email;
            this.IsActive = true;
        }
    }
}
