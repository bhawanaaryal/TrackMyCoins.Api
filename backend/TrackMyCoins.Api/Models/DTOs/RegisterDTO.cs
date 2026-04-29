using System.ComponentModel.DataAnnotations;

namespace TrackMyCoins.Api.Models.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_%+-]+@[a-zA-Z0-9-]+\.[a-zA-Z]{2,}$", ErrorMessage ="Wrong Email Format")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@$%&*])[A-Za-z0-9!@$%&*]{8,}$", ErrorMessage ="Password must contain at least one uppercase letter, one lowercase letter, one digit and one special symbol.")]
        public string Password { get; set; }
    }
}
