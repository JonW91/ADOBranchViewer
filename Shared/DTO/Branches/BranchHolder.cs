namespace Shared.DTO.Branches;

public record BranchHolder(Guid ProjectId,
    Repos.Value Repository,
    Value Branch);