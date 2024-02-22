using System.ComponentModel.DataAnnotations;

namespace Back_end.Models
{
    public class LoginRequests
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }

    }

    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
