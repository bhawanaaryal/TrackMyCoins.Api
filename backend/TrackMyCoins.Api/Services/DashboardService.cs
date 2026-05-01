using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using TrackMyCoins.Api.Data;
using TrackMyCoins.Api.Models.Entities;
using TrackMyCoins.Api.Services.Interfaces;

namespace TrackMyCoins.Api.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;
        public DashboardService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<object> GetDashboard(int userId, int month, int year)
        {
            var filteredExpense = _context.Expenses.Include(e => e.Category)
                .Where(e => e.UserId == userId && e.Date.Month == month && e.Date.Year == year);

            var totalSpent = await filteredExpense.SumAsync(e => e.Amount);

            var budget = await _context.Budgets.FirstOrDefaultAsync(b => b.UserId == userId && b.Month == month && b.Year == year);

            var categoryBreakdown = await _context.Expenses.Where(e => e.UserId == userId).GroupBy(e => e.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    total = g.Sum(e => e.Amount)
                })
                .ToListAsync();

            return (new
            {
                totalSpent,
                budget = budget?.Amount ?? 0,
                remaining = (budget?.Amount ?? 0) - totalSpent,
                categoryBreakdown
            });
        }
    }
}
