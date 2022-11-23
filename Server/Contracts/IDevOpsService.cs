using Shared.DTO.Branches;
using Shared.DTO.Projects;
using Shared.DTO.Repos;

namespace Server.Contracts;

public interface IDevOpsService
{
    Task<AdoProjectsHolder?> ReturnAllAdoProjects();
    Task<AdoReposHolder?> ReturnAllAdoReposForProject(string project);
    Task<AdoBranchesHolder?>? ReturnAllAdoBranches(string project, Guid repoId);
}