using Microsoft.AspNetCore.Mvc;
using Model.Contracts;

namespace Server.Controllers;

[Route("[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly ILogger<ProjectController> _logger;
    private readonly IProjectRepository _projectRepo;

    public ProjectController(IProjectRepository projectRepo,
        ILogger<ProjectController> logger)
    {
        _projectRepo = projectRepo;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        var allProjects = await _projectRepo.GetProjects();

        if (allProjects.Any())
        {
            _logger.LogInformation("Returned all DB Projects {allProjects?.Count}", allProjects?.Count);
            return Ok(allProjects);
        }

        _logger.LogWarning("Unable to return Projects from the Database - {Now}", DateTime.Now);
        return BadRequest("Unable to return any Projects from Database");
    }
}