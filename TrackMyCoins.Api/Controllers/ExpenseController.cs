using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackMyCoins.Api.Data;
using TrackMyCoins.Api.Models.Entities;
using TrackMyCoins.Api.Models.DTOs;
using System.Security.Claims;

[Route("api/expenses")]
[ApiController]
[Authorize]
public class ExpenseController : ControllerBase
{
    private readonly AppDbContext _context;

    public ExpenseController(AppDbContext context)
    {
        _context = context;
    }

    private int GetUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userId);
    }
    [HttpPost]
    public IActionResult CreateExpense(CreateExpenseDTO dto)
    {
        var userId = GetUserId();

        var expense = new Expense
        {
            Title = dto.Title,
            Amount = dto.Amount,
            Date = dto.Date,
            CategoryId = dto.CategoryId,
            UserId = userId
        };

        _context.Expenses.Add(expense);
        _context.SaveChanges();

        return Ok(new { message = "Expense added successfully" });

    }
        [HttpGet]
        public IActionResult GetExpenses()
        {
            var userId = GetUserId();

        var expenses = _context.Expenses
            .Where(e => e.UserId == userId)
            .Select(e => new
            {
                e.Id,
                e.Title,
                e.Amount,
                e.Date,
                CategoryName = e.Category.Name
            })
            .ToList();

        return Ok(expenses);
        }

    [HttpDelete("{id}")]
    public IActionResult DeleteExpense(int id)
    {
        var userId = GetUserId();

        var expense = _context.Expenses
            .FirstOrDefault(e => e.Id == id && e.UserId == userId);

        if (expense == null)
            return NotFound();

        _context.Expenses.Remove(expense);
        _context.SaveChanges();

        return Ok(new { message = "Deleted successfully" });
    }

    [HttpPut("{id}")]
    public IActionResult UpdateExpense(int id, CreateExpenseDTO dto)
    {
        var userId = GetUserId();

        var expense = _context.Expenses
            .FirstOrDefault(e => e.Id == id && e.UserId == userId);

        if (expense == null)
            return NotFound();

        expense.Title = dto.Title;
        expense.Amount = dto.Amount;
        expense.Date = dto.Date;
        expense.CategoryId = dto.CategoryId;

        _context.SaveChanges();

        return Ok(new { message = "Updated successfully" });
    }

}