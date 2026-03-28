using System.ComponentModel.DataAnnotations.Schema;

namespace TrackMyCoins.Api.Models.Entities
{
    public class Budget
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }   

        public User User { get; set; }
    }
}
