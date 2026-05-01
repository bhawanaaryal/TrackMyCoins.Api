using TrackMyCoins.Api.Models.DTOs;
using TrackMyCoins.Api.Models.Entities;

namespace TrackMyCoins.Api.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<object>> GetAllUsersAsync();
        Task<object> GetUserDetailsAsync(int userId);
        Task<bool> EditUserAsync(int userId, UpdateUserDTO updated);
        Task<bool> DeleteUserAsync(int userId);
        Task<bool> EditExpenseAsync(int expenseId, CreateExpenseDTO updated);
        Task<bool> DeleteExpenseAsync(int expenseId);
        Task<bool> EditBudgetAsync(int budgetId, CreateBudgetDTO updated);
        Task<bool> DeleteBudgetAsync(int budgetId);

    }
}
