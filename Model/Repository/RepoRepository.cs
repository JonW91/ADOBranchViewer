using Dapper;
using Model.Context;
using Model.Contracts;
using Shared.DTO.Repos;
using Shared.Models;

namespace Model.Repository;

public class RepoRepository : IRepoRepository
{
    private readonly IDapperContext _context;

    public RepoRepository(IDapperContext context)
    {
        _context = context;
    }

    public async Task<List<RepoModel>> GetRepos()
    {
        const string query =
            "SELECT [Id],[Name],[Url],[ProjectId],[ProjectName],[ProjectDescription],[ProjectLastUpdateTime],[DefaultBranch],[Size] FROM [AdoBranchViewer].[dbo].[Repos]";
        using var connection = _context.CreateConnection();
        var repos = await connection.QueryAsync<RepoModel>(query);
        return repos.ToList();
    }

    public async Task<List<Guid>> GetRepoIds()
    {
        const string query = "SELECT [Id] FROM [AdoBranchViewer].[dbo].[Repos]";
        using var connection = _context.CreateConnection();
        var repos = await connection.QueryAsync<Guid>(query);
        return repos.ToList();
    }

    public async Task InsertRepoData(IEnumerable<Value> adoReposHolder)
    {
        var repositories = await GetRepoIds();
        foreach (var repo in CovertToFlattenedType(adoReposHolder).Where(o => !repositories.Contains(o.Id)))
        {
            const string query =
                "INSERT INTO [dbo].[Repos] ([Id] ,[Name],[Url],[ProjectId],[ProjectName],[ProjectDescription],[ProjectUrl],[ProjectState],[ProjectRevision],[ProjectVisibility],[ProjectLastUpdateTime],[DefaultBranch],[Size],[RemoteUrl],[SshUrl],[WebUrl],[IsDisabled])" +
                " VALUES (@Id ,@Name,@Url,@ProjectId,@ProjectName,@Description,@ProjectUrl,@State,@Revision,@Visibility,@LastUpdateTime,@DefaultBranch,@Size,@RemoteUrl,@SshUrl,@WebUrl,@IsDisabled)";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, repo);
        }
    }

    private static IEnumerable<Shared.DTO.Repos.Repository> CovertToFlattenedType(
        IEnumerable<Value> adoReposHolder)
    {
        return adoReposHolder.Select(o => new Shared.DTO.Repos.Repository(o.Id,
            o.Name,
            o.Url,
            o.DefaultBranch,
            o.Size,
            o.RemoteUrl,
            o.SshUrl,
            o.WebUrl,
            o.IsDisabled,
            o.Project.Id,
            o.Project.Name,
            o.Project.Description,
            o.Project.Url,
            o.Project.State,
            o.Project.Revision,
            o.Project.Visibility,
            o.Project.LastUpdateTime));
    }
}