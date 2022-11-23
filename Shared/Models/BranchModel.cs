namespace Shared.Models;

public class BranchModel
{
    public string? RepoName { get; set; }
    public string? RepoUrl { get; set; }
    public string? ProjectName { get; set; }
    public string? CommitCommitId { get; set; }
    public string? CommitUrl { get; set; }
    public string? BranchName { get; set; }
    public string? AheadCount { get; set; }
    public string? BehindCount { get; set; }
    public int? DayDifference { get; set; }
    public DateTime? CommitDate { get; set; }
    public bool IsStale { get; set; }
}