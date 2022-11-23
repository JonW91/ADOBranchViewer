using Dapper;
using Model.Context;
using Model.Contracts;
using Shared.Models;

namespace Model.Repository;

public class DashboardRepository : IDashboardRepository
{
    private readonly IDapperContext _context;

    public DashboardRepository(IDapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DashboardModel?>> GetDashboardData()
    {
        var connection = _context.CreateConnection();
        return await connection.QueryAsync<DashboardModel?>("EXECUTE dbo.DashboardData").ConfigureAwait(false);
    }
}