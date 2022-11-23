using Server.Contracts;
using Shared.DTO.Branches;
using Shared.DTO.Projects;
using Shared.DTO.Repos;

namespace Server.Services;

public class DevOpsService : IDevOpsService
{
    private readonly HttpClient _httpClient;
    private readonly string? _organisation;

    public DevOpsService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _organisation = configuration.GetValue<string>("PatCode:Organisation");
        _httpClient = httpClientFactory.CreateClient("DevOpsService");
    }

    public async Task<AdoProjectsHolder?> ReturnAllAdoProjects()
    {
        using var response = await _httpClient.GetAsync(
            $"https://dev.azure.com/{_organisation}/_apis/projects?$top=(300%)&api-version=7.1-preview.1");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AdoProjectsHolder>();
    }

    public async Task<AdoReposHolder?> ReturnAllAdoReposForProject(string projectID)
    {
        using var response = await _httpClient.GetAsync(
            $"https://dev.azure.com/{_organisation}/{projectID}/_apis/git/repositories?-version=7.1-preview.1");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AdoReposHolder>();
    }

    public async Task<AdoBranchesHolder?>? ReturnAllAdoBranches(string projectID, Guid repoId)
    {
        using var response = await _httpClient.GetAsync(
            $"https://dev.azure.com/{_organisation}/{projectID}/_apis/git/repositories/{repoId.ToString()}/stats/branches?api-version=7.1-preview.1");
        if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<AdoBranchesHolder>();

        return null;
    }
}