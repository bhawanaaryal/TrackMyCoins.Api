using System.Reflection.Metadata.Ecma335;
using Microsoft.Extensions.Configuration.UserSecrets;
using TrackMyCoins.Api.Data;
using TrackMyCoins.Api.Models.DTOs;
using TrackMyCoins.Api.Models.Entities;
using TrackMyCoins.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TrackMyCoins.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category> AddCategoryAsync(CreateCategoryDTO dto, int userId)
        {
                var category = new Category()
                {
                    Name = dto.Name,
                    UserId = userId
                };

                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return category;
        }


        public async Task<IEnumerable<object>> GetCategoryAsync(int userId)
        {
            return await _context.Categories
                            .Where(c => c.UserId == null || c.UserId == userId)
                            .Select(c => new
                            {
                                c.Id,
                                c.Name
                            })
                            .ToListAsync();
        }
    }
}
