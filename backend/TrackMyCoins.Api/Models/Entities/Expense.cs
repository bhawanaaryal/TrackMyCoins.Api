using System.ComponentModel.DataAnnotations.Schema;

namespace TrackMyCoins.Api.Models.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Column(TypeName = "decimal(18,2)")] 
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public Category Category { get; set; }
        
    }
}
