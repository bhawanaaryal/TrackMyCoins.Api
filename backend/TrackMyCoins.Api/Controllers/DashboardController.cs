using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackMyCoins.Api.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using TrackMyCoins.Api.Services.Interfaces;

[Route("api/dashboard")]
[ApiController]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    private int GetUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userId);
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary(int month, int year)
    {
        var userId = GetUserId();

        var summary = await _dashboardService.GetDashboard(userId, month, year);
        
        return Ok(summary);
    }


}