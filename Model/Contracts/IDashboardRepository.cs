using Shared.Models;

namespace Model.Contracts;

public interface IDashboardRepository
{
    Task<IEnumerable<DashboardModel?>> GetDashboardData();
}