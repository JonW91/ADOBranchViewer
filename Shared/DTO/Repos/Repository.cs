namespace Shared.DTO.Repos;

public record Repository(Guid Id,
    string Name,
    string Url,
    string DefaultBranch,
    int Size,
    string RemoteUrl,
    string SshUrl,
    string WebUrl,
    bool IsDisabled,
    Guid ProjectId,
    string ProjectName,
    string Description,
    string ProjectUrl,
    string State,
    int Revision,
    string Visibility,
    DateTime LastUpdateTime);