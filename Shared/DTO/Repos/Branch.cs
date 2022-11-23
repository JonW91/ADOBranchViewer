namespace Shared.DTO.Repos;

public record Branch(string? Name,
    int AheadCount,
    int BehindCount,
    bool IsBaseVersion,
    string BranchTreeId,
    string BranchCommitId,
    string BranchComment,
    string BranchParents,
    string BranchUrl,
    string AuthorName,
    string AuthorEmail,
    DateTime AuthorDate,
    string CommitterName,
    string CommitterEmail,
    DateTime CommitterDate,
    Guid RepositoryId,
    Guid ProjectId
);