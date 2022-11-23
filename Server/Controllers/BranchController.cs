using Microsoft.AspNetCore.Mvc;
using Model.Contracts;

namespace Server.Controllers;

[Route("[controller]")]
[ApiController]
public class BranchController : ControllerBase
{
    private readonly IBranchRepository _branchRepo;
    private readonly ILogger<BranchController> _logger;

    public BranchController(IBranchRepository branchRepo, ILogger<BranchController> logger)
    {
        _branchRepo = branchRepo;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetBranches()
    {
        var branches = await _branchRepo.GetBranches();
        if (branches.Any())
        {
            _logger.LogInformation("Loaded {BranchesCount} branches", branches.Count);
            return Ok(branches);
        }

        _logger.LogWarning("No Branches returned");
        return BadRequest("Unable to return Branches from DB");
    }
}