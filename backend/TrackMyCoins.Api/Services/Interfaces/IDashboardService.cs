namespace TrackMyCoins.Api.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<object> GetDashboard (int userId, int month, int year);   
    }
}
