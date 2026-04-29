using System.ComponentModel.DataAnnotations;

namespace TrackMyCoins.Api.Models.DTOs
{
    public class CreateExpenseDTO
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal? Amount { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public int? CategoryId { get; set; }
    }
}
