namespace Shared.Models;

public class RepoModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
    public Guid ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public string? ProjectDescription { get; set; }
    public string? DefaultBranch { get; set; }
    public int? Size { get; set; }
}