namespace Shared.DTO.Branches;

public record Commit(
    string TreeId,
    string CommitId,
    Author Author,
    Committer Committer,
    string Comment,
    List<string> Parents,
    string? Url);