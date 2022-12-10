using System.ComponentModel.DataAnnotations;

namespace TodoApp.Api.Models.Auth
{
    public class LoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
