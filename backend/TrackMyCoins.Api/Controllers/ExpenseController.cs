using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackMyCoins.Api.Data;
using TrackMyCoins.Api.Models.Entities;
using TrackMyCoins.Api.Models.DTOs;
using System.Security.Claims;
using TrackMyCoins.Api.Services.Interfaces;

[Route("api/expenses")]
[ApiController]
[Authorize]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    private int GetUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userId);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateExpense(CreateExpenseDTO dto)
    {
        var userId = GetUserId();
        await _expenseService.AddExpenseAsync(dto, userId);
        
        return Ok(new { message = "Expense added successfully" });

    }
    [HttpGet]
    public async Task<IActionResult> GetExpenses()
    {
        var userId = GetUserId();

        var expense = await _expenseService.GetExpenseAsync(userId);
        return Ok(expense);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        var userId = GetUserId();

        var success = await _expenseService.DeleteExpenseAsync(userId, id);
        if (!success)
        {
            return BadRequest();
        }
        return Ok(new { message = "Deleted successfully" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(int id, CreateExpenseDTO dto)
    {
        var userId = GetUserId();
        var submit = await _expenseService.UpdateExpenseAsync(userId, id, dto);
        if (!submit)
        {
            return BadRequest();    
        }
        return Ok(new { message = "Updated successfully" });
    }

}