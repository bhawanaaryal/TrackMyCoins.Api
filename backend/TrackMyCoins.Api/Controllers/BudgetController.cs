using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackMyCoins.Api.Data;
using TrackMyCoins.Api.Models.Entities;
using TrackMyCoins.Api.Models.DTOs;
using System.Security.Claims;
using TrackMyCoins.Api.Services.Interfaces;

[Route("api/budgets")]
[ApiController]
[Authorize]
public class BudgetController : ControllerBase
{
    private readonly IBudgetService _budgetService;

    public BudgetController(IBudgetService budgetService)
    {
        _budgetService = budgetService;
    }


    [HttpPost]
    public async  Task<IActionResult> SetBudget(CreateBudgetDTO dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        await _budgetService.AddBudgetAsync(userId, dto);
        return Ok(new { message = "Budget saved" });
    }

    [HttpGet]
    public async Task<IActionResult> GetBudgetAsync(int month, int year)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var budget = await _budgetService.GetBudgetAsync(userId, month, year);
        if (budget == null)
        {
            return NotFound();
        }

        return Ok(budget);
    }
}