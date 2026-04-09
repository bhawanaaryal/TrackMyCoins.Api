namespace TrackMyCoins.Api.Models.DTOs
{
    public class CreateBudgetDTO
    {
        public decimal Amount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
