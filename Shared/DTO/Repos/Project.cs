namespace Shared.DTO.Repos;

public record Project(
    Guid Id,
    string Name,
    string Description,
    string Url,
    string State,
    int Revision,
    string Visibility,
    DateTime LastUpdateTime);