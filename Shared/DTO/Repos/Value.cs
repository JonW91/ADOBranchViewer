namespace Shared.DTO.Repos;

public record Value(
    Guid Id,
    string Name,
    string Url,
    Project Project,
    string DefaultBranch,
    int Size,
    string RemoteUrl,
    string SshUrl,
    string WebUrl,
    bool IsDisabled);