namespace Shared.DTO.Branches;

public record Committer(
    string Name,
    string Email,
    DateTime Date);