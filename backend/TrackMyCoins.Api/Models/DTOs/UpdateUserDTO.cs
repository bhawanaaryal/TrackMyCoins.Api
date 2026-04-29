using System.ComponentModel.DataAnnotations;

namespace TrackMyCoins.Api.Models.DTOs
{
    public class UpdateUserDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_%+-]+@[a-zA-Z0-9-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Wrong Email Format")]
        
        public string? Email { get; set; }
        [Required]
        public bool? IsAdmin { get; set; }
    }
}
