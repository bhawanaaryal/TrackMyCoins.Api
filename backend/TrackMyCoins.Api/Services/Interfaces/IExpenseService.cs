using TrackMyCoins.Api.Models.DTOs;
using TrackMyCoins.Api.Models.Entities;

namespace TrackMyCoins.Api.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<Expense> AddExpenseAsync(CreateExpenseDTO dto, int userId);
        Task<IEnumerable<object>> GetExpenseAsync(int userId);
        Task<bool> DeleteExpenseAsync(int userId, int id);
        Task<bool> UpdateExpenseAsync(int userId, int id, CreateExpenseDTO dto);    
    }
}
