#nullable enable
using Dapper;
using Model.Context;
using Model.Contracts;
using Shared.DTO.Branches;
using Shared.DTO.Repos;
using Shared.Models;
using Value = Shared.DTO.Branches.Value;

namespace Model.Repository;

public class BranchRepository : IBranchRepository
{
    private readonly IDapperContext _context;

    public BranchRepository(IDapperContext context)
    {
        _context = context;
    }

    public async Task<List<BranchModel>> GetBranches()
    {
        const string query =
            "SELECT  [RepoName], [RepoUrl], [ProjectName], [CommitCommitId], [CommitUrl], [BranchName], [AheadCount], [BehindCount], [CommitDate], [IsStale], [DayDifference]  FROM [AdoBranchViewer].[dbo].[vw_BranchesWithRepos]";
        using var connection = _context.CreateConnection();

        var branch = await connection.QueryAsync<BranchModel>(query);

        return branch.ToList();
    }

    public async Task InsertBranchData(List<BranchHolder> adoBranchesHolder)
    {
        if (!adoBranchesHolder.Any()) return;


        foreach (var branchHolder in adoBranchesHolder)
        {
            var toInsert =
                CovertToFlattenedType(branchHolder.Branch, branchHolder.ProjectId, branchHolder.Repository.Id);
            if (toInsert == null) continue;

            const string query = "INSERT INTO [AdoBranchViewer].[dbo].[Branches]" +
                                 "([CommitTreeId],[CommitCommitId]," +
                                 "[CommitAuthorName],[CommitAuthorEmail], [CommitAuthorDate], [CommitCommitterName], [CommitCommitterEmail], " +
                                 "[CommitCommitterDate], [CommitComment], [CommitParents0], [CommitUrl], [Name], [AheadCount], [BehindCount], " +
                                 "[IsBaseVersion], [RepositoryId], [ProjectId])" +
                                 " Values " +
                                 "(@BranchTreeId, @BranchCommitId, @AuthorName,@AuthorEmail, @AuthorDate, @CommitterName, @CommitterEmail, " +
                                 "@CommitterDate, @BranchComment, @BranchParents, @BranchUrl, @Name, @AheadCount, @BehindCount, " +
                                 "@IsBaseVersion, @RepositoryId, @ProjectId)";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, toInsert);
        }
    }

    public async Task<List<DuplicateBranchModel>?> MultipleBranches()
    {
        var connection = _context.CreateConnection();
        var duplicateBranches = await connection
            .QueryAsync<DuplicateBranchModel>("EXECUTE dbo.RepositoryMultipleBranches")
            .ConfigureAwait(false);
        return duplicateBranches.ToList();
    }

    private static Branch? CovertToFlattenedType(Value branch, Guid projectId, Guid repositoryId)
    {
        if (branch?.Commit?.Parents != null)
            return new Branch(branch.Name,
                branch.AheadCount,
                branch.BehindCount,
                branch.IsBaseVersion,
                branch.Commit?.TreeId,
                branch.Commit?.CommitId,
                branch.Commit?.Comment,
                string.Join(" | ", branch?.Commit?.Parents),
                branch.Commit?.Url ?? "",
                branch.Commit?.Author?.Name,
                branch.Commit?.Author?.Email,
                branch.Commit?.Author?.Date ?? DateTime.Now,
                branch.Commit?.Committer?.Name,
                branch.Commit?.Committer?.Email,
                branch.Commit?.Committer?.Date ?? DateTime.Now,
                repositoryId,
                projectId);

        return null;
    }
}