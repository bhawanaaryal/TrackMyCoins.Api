using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackMyCoins.Api.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

[Route("api/dashboard")]
[ApiController]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    private int GetUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userId);
    }

    [HttpGet("summary")]
    public IActionResult GetSummary(int month, int year)
    {
        var userId = GetUserId();

        var expenses = _context.Expenses
            .Include(e => e.Category)
            .Where(e => e.UserId == userId &&
                        e.Date.Month == month &&
                        e.Date.Year == year)
            .ToList();

        var totalSpent = expenses.Sum(e => e.Amount);

        var budget = _context.Budgets
            .FirstOrDefault(b => b.UserId == userId &&
                                 b.Month == month &&
                                 b.Year == year);

        var categoryBreakdown = expenses
            .GroupBy(e => e.Category.Name)
            .Select(g => new
            {
                category = g.Key,
                total = g.Sum(e => e.Amount)
            })
            .ToList();

        return Ok(new
        {
            totalSpent,
            budget = budget?.Amount ?? 0,
            remaining = (budget?.Amount ?? 0) - totalSpent,
            categoryBreakdown
        });
    }
}