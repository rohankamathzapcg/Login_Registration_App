using System.ComponentModel.DataAnnotations;

namespace Back_end.Models
{
    public class RegistrationRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Role { get; set; }

    }

    public class RegistrationResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
