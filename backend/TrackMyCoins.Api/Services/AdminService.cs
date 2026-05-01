using Microsoft.EntityFrameworkCore;
using TrackMyCoins.Api.Data;
using TrackMyCoins.Api.Models.DTOs;
using TrackMyCoins.Api.Models.Entities;
using TrackMyCoins.Api.Services.Interfaces;

namespace TrackMyCoins.Api.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _context;
        public AdminService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteBudgetAsync(int budgetId)
        {
            var budget = await _context.Budgets.FindAsync(budgetId);
            if (budget == null)
            {
                return false;
            }

            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
            return true;    
        }

        public async Task<bool> DeleteExpenseAsync(int expenseId)
        {
            var expense = await _context.Expenses.FindAsync(expenseId);
            if (expense == null)
            {
                return false;
            }
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync(); 
            return true;    
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditBudgetAsync(int budgetId, CreateBudgetDTO updated)
        {
            var budget = await _context.Budgets.FindAsync(budgetId);
            if(budget == null)
            {
                return false;
            }

            budget.Amount = updated.Amount; 
            budget.Month = updated.Month;   
            budget.Year = updated.Year;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditExpenseAsync(int expenseId, CreateExpenseDTO updated)
        {
            var expense = await _context.Expenses.FindAsync(expenseId);
            if (expense == null)
            {
                return false;
            }
            expense.Title = updated.Title;
            expense.Amount = updated.Amount;
            expense.Date = updated.Date;
            expense.CategoryId = updated.CategoryId;

            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> EditUserAsync(int userId, UpdateUserDTO updated)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }

            user.Name = updated.Name;
            user.Email = updated.Email;
            user.IsAdmin = updated.IsAdmin;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<object>> GetAllUsersAsync()
        {
            return await _context.Users.Select(u => new
            {
                u.Id,
                u.Name,
                u.Email,
                u.IsAdmin
            }).ToListAsync();
        }

        public async Task<object> GetUserDetailsAsync(int userId)
        {
            var details = await _context.Users.Where(u => u.Id == userId)
                .Select(u => new
                {
                    u.Id,
                    u.Name,
                    u.Email,
                    Expenses = u.Expenses.Select(e => new { e.Id, e.Amount, e.Date, e.CategoryId }),
                    Budget = u.Budgets.Select(b => new { b.Id, b.Amount, b.Month, b.Year }),
                    Category = u.Expenses.Select(e => e.Category)
                    .Distinct()
                    .Select(c => new { c.Id, c.Name })
                })
                .FirstOrDefaultAsync(); 
            return details;
        }
    }
}
