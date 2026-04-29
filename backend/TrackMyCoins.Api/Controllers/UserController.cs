using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TrackMyCoins.Api.Data;
using TrackMyCoins.Api.Models.Entities;
using TrackMyCoins.Api.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using TrackMyCoins.Api.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrackMyCoins.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger _logger;
        public UserController(IAuthService authService, ILogger<UserController> logger)
        {
            
            _authService = authService;
            _logger = logger;
        }

        private bool IsAdmin()
        {
            var isAdmin = User.Claims.FirstOrDefault(c => c.Type == "IsAdmin")?.Value;
            return isAdmin == "True";
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            _logger.LogInformation("Register attempt for {Email}", dto.Email);

            var user = await _authService.RegisterAsync(dto);
            if (user is null) 
            {
                return BadRequest(new { message = "User doesn't exist" });
            }
            _logger.LogInformation("User registered successfully: {Email}", dto.Email);
            return Ok(new { message = "User registered successfully!!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto )
        {
            _logger.LogInformation("Login attempt for {Email}", dto.Email);
            var token = await _authService.LoginAsync(dto);
            if (token is null)
            {
                return BadRequest(new { message = "Wrong username or password." });
            }
            _logger.LogInformation("User logged in successfully");
             return Ok(new { token = token });


            }
      
        //private int GetUserId()
        //{
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    return int.Parse(userId);
        //}

        //[Authorize]
        //[HttpGet("admin-dashboard")]
        //public IActionResult AdminDashboard()
        //{
        //    var isAdmin = User.Claims.FirstOrDefault(c => c.Type == "IsAdmin")?.Value;
        //    if (isAdmin != "True")
        //        return Forbid(); 

        //    return Ok(new { message = "Welcome, Admin!" });
        //}

        //[Authorize]
        //[HttpGet("all-users")]
        //public IActionResult GetAllUsers()
        //{
        //    if (!IsAdmin()) return Forbid();

        //    var users = _context.Users
        //        .Select(u => new { u.Id, u.Name, u.Email, u.IsAdmin })
        //        .ToList();

        //    return Ok(users);
        //}

        //[Authorize]
        //[HttpGet("{userId}/details")]
        //public IActionResult GetUserDetails(int userId)
        //{
        //    if (!IsAdmin()) return Forbid();

        //    var user = _context.Users
        //        .Where(u => u.Id == userId)
        //        .Select(u => new
        //        {
        //            u.Id,
        //            u.Name,
        //            u.Email,
        //            Expenses = u.Expenses.Select(e => new { e.Id, e.Title, e.Amount, e.Date, e.CategoryId }),
        //            Budgets = u.Budgets.Select(b => new { b.Id, b.Amount, b.Month }),
        //            Categories = u.Expenses
        //        .Select(e => e.Category)
        //        .Distinct()
        //        .Select(c => new { c.Id, c.Name })
        //        })
        //        .FirstOrDefault();

        //    if (user == null) return NotFound();

        //    return Ok(user);
        //}

        //[Authorize]
        //[HttpPut("{userId}")]
        //public IActionResult EditUser(int userId, UpdateUserDTO updated)
        //{
        //    if (!IsAdmin()) return Forbid();

        //    var user = _context.Users.Find(userId);
        //    if (user == null) return NotFound();

        //    user.Name = updated.Name;
        //    user.Email = updated.Email;
        //    user.IsAdmin = updated.IsAdmin;

        //    _context.SaveChanges();
        //    return Ok(user);
        //}

        //[Authorize]
        //[HttpDelete("{userId}")]
        //public IActionResult DeleteUser(int userId)
        //{
        //    if (!IsAdmin()) return Forbid();

        //    var user = _context.Users.Find(userId);
        //    if (user == null) return NotFound();

        //    _context.Users.Remove(user);
        //    _context.SaveChanges();
        //    return Ok();
        //}

        //[Authorize]
        //[HttpPut("expenses/{expenseId}")]
        //public IActionResult EditExpense(int expenseId, Expense updated)
        //{
        //    if (!IsAdmin()) return Forbid();

        //    var expense = _context.Expenses.Find(expenseId);
        //    if (expense == null) return NotFound();

        //    expense.Title = updated.Title;
        //    expense.Amount = updated.Amount;
        //    expense.Date = updated.Date;
        //    expense.CategoryId = updated.CategoryId;

        //    _context.SaveChanges();
        //    return Ok(expense);
        //}

        //[Authorize]
        //[HttpDelete("expenses/{expenseId}")]
        //public IActionResult DeleteExpense(int expenseId)
        //{
        //    if (!IsAdmin()) return Forbid();

        //    var expense = _context.Expenses.Find(expenseId);
        //    if (expense == null) return NotFound();

        //    _context.Expenses.Remove(expense);
        //    _context.SaveChanges();
        //    return Ok();
        //}

        //[Authorize]
        //[HttpPut("budgets/{budgetId}")]
        //public IActionResult EditBudget(int budgetId, Budget updated)
        //{
        //    if (!IsAdmin()) return Forbid();

        //    var budget = _context.Budgets.Find(budgetId);
        //    if (budget == null) return NotFound();

        //    budget.Amount = updated.Amount;
        //    budget.Month = updated.Month;

        //    _context.SaveChanges();
        //    return Ok(budget);
        //}

        //[Authorize]
        //[HttpDelete("budgets/{budgetId}")]
        //public IActionResult DeleteBudget(int budgetId)
        //{
        //    if (!IsAdmin()) return Forbid();

        //    var budget = _context.Budgets.Find(budgetId);
        //    if (budget == null) return NotFound();

        //    _context.Budgets.Remove(budget);
        //    _context.SaveChanges();
        //    return Ok();
        //}
    }
}
