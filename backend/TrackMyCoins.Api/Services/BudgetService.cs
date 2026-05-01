using TrackMyCoins.Api.Data;
using TrackMyCoins.Api.Models.DTOs;
using TrackMyCoins.Api.Models.Entities;
using TrackMyCoins.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TrackMyCoins.Api.Services
{
    public class BudgetService: IBudgetService
    {
        private readonly AppDbContext _context;

        public BudgetService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Budget> AddBudgetAsync(int userId, CreateBudgetDTO dto)
        {
            var existingBudget = await _context.Budgets.FirstOrDefaultAsync(b => b.UserId == userId && b.Month == dto.Month && b.Year == dto.Year);
            if (existingBudget != null)
            {
                existingBudget.Amount = dto.Amount;
                await _context.SaveChangesAsync();
                return existingBudget;
            }
            var budget = new Budget
                {
                    Amount = dto.Amount,
                    Year = dto.Year,
                    Month = dto.Month,
                    UserId = userId
                };
            await _context.Budgets.AddAsync(budget);
            await _context.SaveChangesAsync();
            return budget;
        }

        public async Task<Budget> GetBudgetAsync(int userId, int month, int year)
        {
            return await _context.Budgets.FirstOrDefaultAsync(b => b.UserId == userId && b.Year == year && b.Month == month);
        }
    }

}

