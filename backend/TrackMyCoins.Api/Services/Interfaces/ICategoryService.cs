using TrackMyCoins.Api.Models.DTOs;
using TrackMyCoins.Api.Models.Entities;

namespace TrackMyCoins.Api.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> AddCategoryAsync(CreateCategoryDTO dto, int userId);
        Task<IEnumerable<object>> GetCategoryAsync(int userId);
        
    }
}
