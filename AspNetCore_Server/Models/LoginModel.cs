using System.ComponentModel.DataAnnotations;

namespace AspNetCore_Server.Models
{
    public class LoginModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
