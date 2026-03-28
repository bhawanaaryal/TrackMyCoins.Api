namespace TrackMyCoins.Api.Models.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public Category Category { get; set; }
        
    }
}
