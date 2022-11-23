using Microsoft.AspNetCore.Mvc;
using Model.Contracts;

namespace Server.Controllers;

[Route("[controller]")]
[ApiController]
public class ProjectMultipleBranchController : ControllerBase
{
    private const string NoMultipleBranchReturned = "No Multiple Branches returned from the DB";
    private readonly IBranchRepository _branchRepo;
    private readonly ILogger<ProjectMultipleBranchController> _logger;

    public ProjectMultipleBranchController(IBranchRepository branchRepo,
        ILogger<ProjectMultipleBranchController> logger)
    {
        _branchRepo = branchRepo;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetBranches()
    {
        var branches = await _branchRepo.MultipleBranches();
        if (branches != null && branches.Any())
        {
            _logger.LogInformation("Loaded Multiple Branches branches");
            return Ok(branches);
        }

        _logger.LogWarning(NoMultipleBranchReturned);
        return BadRequest(NoMultipleBranchReturned);
    }
}