using TrackMyCoins.Api.Models.DTOs;
using TrackMyCoins.Api.Models.Entities;

namespace TrackMyCoins.Api.Services.Interfaces
{
    public interface IBudgetService
    {
        Task<Budget> AddBudgetAsync(int userId, CreateBudgetDTO dTO);
        Task<Budget> GetBudgetAsync(int userId, int month, int year);
    }
}
