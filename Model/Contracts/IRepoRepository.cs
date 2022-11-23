using Shared.DTO.Repos;
using Shared.Models;

namespace Model.Contracts;

public interface IRepoRepository
{
    public Task<List<RepoModel>> GetRepos();

    public Task InsertRepoData(IEnumerable<Value> adoReposHolder);
    public Task<List<Guid>> GetRepoIds();
}