using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackMyCoins.Api.Models.DTOs;
using TrackMyCoins.Api.Models.Entities;
using TrackMyCoins.Api.Services.Interfaces;

namespace TrackMyCoins.Api.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }



        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{userId}/details")]
        public async Task<IActionResult> GetUserDetails(int userId)
        {
            var details = await _adminService.GetUserDetailsAsync(userId);
            if (details == null)
            {
                return NotFound();
            }
            return Ok(details);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, UpdateUserDTO dto)
        {
            var success = await _adminService.EditUserAsync(userId, dto);
            if (!success)
            {
                return NotFound();
            }
            return Ok(new { message = "Updated successfully." });
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var success = await _adminService.DeleteUserAsync(userId);
            if (!success)
            {
                return NotFound();
            }
            return Ok(new { message = "Deleted successfully." });
        }

        [HttpPut("expenses/{expenseId}")]
        public async Task<IActionResult> UpdateExpense(int expenseId, CreateExpenseDTO expense)
        {
            var success = await _adminService.EditExpenseAsync(expenseId, expense);
            if (!success)
            {
                return NotFound();
            }
            return Ok(new { message = "Updated successfully." });
        }

        [HttpDelete("expenses/{expenseId}")]
        public async Task<IActionResult> DeleteExpense(int expenseId)
        {
            var success = await _adminService.DeleteExpenseAsync(expenseId);
            if (!success)
            {
                return NotFound();
            }
            return Ok(new { message = "Deleted successfully." });
        }

        [HttpPut("budgets/{budgetId}")]
        public async Task<IActionResult> UpdateBudget(int budgetId, CreateBudgetDTO budget)
        {
            var success = await _adminService.EditBudgetAsync(budgetId, budget);
            if (!success)
            {
                return NotFound();
            }
            return Ok(new { message = "Updated successfully." });
        }

        [HttpDelete("budgets/{budgetId}")]
        public async Task<IActionResult> DeleteBudget(int budgetId)
        {
            var success = await _adminService.DeleteBudgetAsync(budgetId);
            if (!success)
            {
                return NotFound();
            }
            return Ok(new { message = "Deleted successfully." });
        }
    }
}
