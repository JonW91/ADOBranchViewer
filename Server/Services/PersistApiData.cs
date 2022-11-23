using Model.Contracts;
using Server.Contracts;
using Shared.DTO.Branches;
using Value = Shared.DTO.Repos.Value;

namespace Server.Services;

public class PersistApiData : IPersistApiData
{
    private readonly IBranchRepository _branchRepository;
    private readonly IDevOpsService _devOpsService;
    private readonly ILogger<PersistApiData> _logger;
    private readonly IProjectRepository _projectRepository;
    private readonly IRepoRepository _repoRepository;
    private readonly List<RepositoryHolder> AzureDevOpsRepositories = new();
    private readonly List<BranchHolder> Branches = new();

    public PersistApiData(IDevOpsService devOpsService, ILogger<PersistApiData> logger,
        IBranchRepository branchRepository, IProjectRepository projectRepository, IRepoRepository repoRepository)
    {
        _devOpsService = devOpsService;
        _logger = logger;
        _branchRepository = branchRepository;
        _projectRepository = projectRepository;
        _repoRepository = repoRepository;
    }

    public async Task<bool> PushApiDataToDatabase()
    {
        var allProjects = await _devOpsService.ReturnAllAdoProjects();


        if (allProjects == null) return false;


        foreach (var project in allProjects.Value)
        {
            var allReposForProject = await _devOpsService.ReturnAllAdoReposForProject(project.Id);
            foreach (var projectRepo in allReposForProject.Value)

                if (project.Name != null)
                    AzureDevOpsRepositories.Add(new RepositoryHolder(project.Name, project.Id, projectRepo));
        }

        foreach (var repository in AzureDevOpsRepositories)
        {
            _logger.LogInformation("Current Project Name - {RepositoryProjectName} - repo Id = {S})",
                repository.ProjectName, repository.Repository.Id.ToString());

            var branches = await _devOpsService.ReturnAllAdoBranches(repository.ProjectName, repository.Repository.Id)!;
            if (branches?.Value == null) continue;

            foreach (var branch in branches.Value)
                Branches.Add(new BranchHolder(Guid.Parse(repository.ProjectId), repository.Repository, branch));
        }

        try
        {
            await _projectRepository.InsertProjectData(allProjects);
            await _repoRepository.InsertRepoData(AzureDevOpsRepositories.Select(o => o.Repository).ToList());
            await _branchRepository.InsertBranchData(Branches);
            return true;
        }
        catch (Exception exception)
        {
            throw new Exception($"Unable to insert API data into the database {exception.Message}", exception);
        }
    }

    private sealed record RepositoryHolder(string ProjectName, string ProjectId, Value Repository);
}