namespace Shared.DTO.Branches;

public record Value(
    Commit Commit,
    string Name,
    int AheadCount,
    int BehindCount,
    bool IsBaseVersion);