using Microsoft.AspNetCore.Mvc;
using Model.Contracts;

namespace Server.Controllers;

[Route("[controller]")]
[ApiController]
public class RepoController : ControllerBase
{
    private readonly ILogger<RepoController> _logger;
    private readonly IRepoRepository _repoRepo;

    public RepoController(IRepoRepository repoRepo, ILogger<RepoController> logger)
    {
        _repoRepo = repoRepo;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetRepos()
    {
        var allRepos = await _repoRepo.GetRepos();
        if (allRepos.Any())
        {
            _logger.LogInformation("Returned all DB Repos {allRepos?.Count}", allRepos.Count);
            return Ok(allRepos);
        }

        _logger.LogWarning("No Repositories to return from the Database - {Now}", DateTime.Now);
        return BadRequest("No Repositories to return from the Database");
    }
}