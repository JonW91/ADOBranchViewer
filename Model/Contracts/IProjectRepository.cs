using Shared.DTO.Projects;
using Shared.Models;

namespace Model.Contracts;

public interface IProjectRepository
{
    Task<List<ProjectModel>> GetProjects();
    Task InsertProjectData(AdoProjectsHolder? adoProjectsHolder);
    Task<List<Guid>> GetProjectIds();
}