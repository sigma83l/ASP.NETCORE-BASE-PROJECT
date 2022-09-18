using AspNetCore_Server.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCore_Server.Models
{
    public class ViewUser 
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email address is required")]
        public string email { get; set; }
        [Required]
        [Phone]
        public string phonenumber { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password confirmation is required")]
        public string Passwordconfirmation { get; set; }
        [Required(ErrorMessage = "username is required")]
        public string username { get; set; }
        [Required(ErrorMessage = "Firstname is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }
        public string bio { get; set; }
        [Required(ErrorMessage = "Image is required")]
        public IFormFile Image { get; set; }
        public string Role { get; set; }
    }
}
