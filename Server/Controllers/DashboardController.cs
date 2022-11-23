using Microsoft.AspNetCore.Mvc;
using Model.Contracts;

namespace Server.Controllers;

[Route("[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private const string NoData = "No Dashboard Data returned from DB";
    private readonly IDashboardRepository _dashboardRepository;
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(IDashboardRepository dashboardRepository, ILogger<DashboardController> logger)
    {
        _dashboardRepository = dashboardRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetDashboard()
    {
        var dashboard = await _dashboardRepository.GetDashboardData();
        if (dashboard.Any())
        {
            _logger.LogInformation("Loaded Dashboard");
            return Ok(dashboard.FirstOrDefault());
        }

        _logger.LogWarning(NoData);
        return BadRequest(NoData);
    }
}