using Microsoft.AspNetCore.Mvc;
using Server.Contracts;

namespace Server.Controllers;

[Route("[controller]")]
[ApiController]
public class PersistBranchesController : ControllerBase
{
    private readonly ILogger<PersistBranchesController> _logger;
    private readonly IPersistApiData _persistApiData;

    public PersistBranchesController(IPersistApiData persistApiData, ILogger<PersistBranchesController> logger)
    {
        _persistApiData = persistApiData;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetBranches()
    {
        var databaseResult = await _persistApiData.PushApiDataToDatabase();
        if (databaseResult)
        {
            _logger.LogInformation("Successfully Persisted Data to the Database at time {Now}", DateTime.Now);
            return Ok();
        }

        _logger.LogWarning("There was an issue insert into the database at time {Now}", DateTime.Now);
        return BadRequest("There was an issue insert into the database");
    }
}