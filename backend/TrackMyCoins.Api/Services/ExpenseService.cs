using TrackMyCoins.Api.Data;
using TrackMyCoins.Api.Models.DTOs;
using TrackMyCoins.Api.Models.Entities;
using TrackMyCoins.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TrackMyCoins.Api.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly AppDbContext _context;

        public ExpenseService(AppDbContext context)
        {
            _context = context; 
        }
        public async Task<Expense> AddExpenseAsync(CreateExpenseDTO dto, int userId)
        {
            var expense = new Expense()
            {
                Title = dto.Title,
                Amount = dto.Amount,
                Date = dto.Date,
                CategoryId = dto.CategoryId,
                UserId = userId
            };
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task<bool> DeleteExpenseAsync(int userId, int id)
        {
            var expense = _context.Expenses.FirstOrDefault(e => e.UserId == userId && e.Id == id);
            if (expense == null)
            {
                return false;
            }

            _context.Expenses.Remove(expense);  
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<object>> GetExpenseAsync(int userId)
        {
            return await _context.Expenses.Where(e =>  userId == e.UserId)
                .Select(e => new
                {
                    e.Id,
                    e.Title,
                    e.Amount,
                    e.Date,
                    categoryName = e.Category.Name
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateExpenseAsync(int userId, int id, CreateExpenseDTO dto)
        {
            var expense = _context.Expenses.FirstOrDefault(e => e.UserId == userId && e.Id == id);
            if (expense == null)
            {
                return false;
            }

            expense.Title = dto.Title;
            expense.Amount = dto.Amount;
            expense.Date = dto.Date;    
            expense.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();  
            return true;    

        }
    }
}
