namespace TrackMyCoins.Api.Models.DTOs
{
    public class CreateExpenseDTO
    {
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
    }
}
