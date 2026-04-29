using System.ComponentModel.DataAnnotations;

namespace TrackMyCoins.Api.Models.DTOs
{
    public class CreateCategoryDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
