using System.ComponentModel.DataAnnotations;

namespace TrackMyCoins.Api.Models.DTOs
{
    public class CreateExpenseDTO
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [Range(0.01, 9999999999999999.99, ErrorMessage = "Amount must be greater than zero")]
        public decimal Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Category ID must be greater than 0")]
        public int CategoryId { get; set; }
    }
}
