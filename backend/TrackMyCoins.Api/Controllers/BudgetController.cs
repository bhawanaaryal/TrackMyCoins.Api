using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackMyCoins.Api.Data;
using TrackMyCoins.Api.Models.Entities;
using TrackMyCoins.Api.Models.DTOs;
using System.Security.Claims;

[Route("api/budgets")]
[ApiController]
[Authorize]
public class BudgetController : ControllerBase
{
    private readonly AppDbContext _context;

    public BudgetController(AppDbContext context)
    {
        _context = context;
    }

    private int GetUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userId);
    }
    [HttpPost]
    public IActionResult SetBudget(CreateBudgetDTO dto)
    {
        var userId = GetUserId();

        var existing = _context.Budgets
            .FirstOrDefault(b => b.UserId == userId && b.Month == dto.Month && b.Year == dto.Year);

        if (existing != null)
        {
            existing.Amount = dto.Amount;
        }
        else
        {
            var budget = new Budget
            {
                UserId = userId,
                Amount = dto.Amount,
                Month = dto.Month,
                Year = dto.Year
            };

            _context.Budgets.Add(budget);
        }

        _context.SaveChanges();

        return Ok(new { message = "Budget saved" });
    }

    [HttpGet]
    public IActionResult GetBudget(int month, int year)
    {
        var userId = GetUserId();

        var budget = _context.Budgets
            .FirstOrDefault(b => b.UserId == userId && b.Month == month && b.Year == year);

        return Ok(budget);
    }
}