using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackMyCoins.Api.Data;
using System.Security.Claims;
using TrackMyCoins.Api.Models.DTOs;
using TrackMyCoins.Api.Models.Entities;

[Route("api/categories")]
[ApiController]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoryController(AppDbContext context)
    {
        _context = context;
    }

    private int GetUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userId);
    }

    [HttpPost]
    public IActionResult AddCategory(CreateCategoryDTO dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var category = new Category
        {
            Name = dto.Name,
            UserId = userId
        };

        _context.Categories.Add(category);
        _context.SaveChanges();

        return Ok(new { message = "Category added!" });
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        var userId = GetUserId();

        var categories = _context.Categories
            .Where(c => c.UserId == null || c.UserId == userId)
            .Select(c => new {
                c.Id,
                c.Name
            })
            .ToList();

        return Ok(categories);
    }
}