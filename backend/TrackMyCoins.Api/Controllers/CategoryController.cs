using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackMyCoins.Api.Data;
using System.Security.Claims;
using TrackMyCoins.Api.Models.DTOs;
using TrackMyCoins.Api.Models.Entities;
using TrackMyCoins.Api.Services.Interfaces;

[Route("api/categories")]
[ApiController]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }


    [HttpPost]
    public async Task<IActionResult> AddCategory(CreateCategoryDTO dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        await _categoryService.AddCategoryAsync(dto, userId);

        return Ok(new { message = "Category added!" });
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var categories = await _categoryService.GetCategoryAsync(userId);

        return Ok(categories);
    }
}