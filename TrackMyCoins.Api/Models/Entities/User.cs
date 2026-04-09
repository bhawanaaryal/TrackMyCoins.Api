namespace TrackMyCoins.Api.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }

        public ICollection<Expense> Expenses { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Budget> Budgets { get; set; }

    }
}
