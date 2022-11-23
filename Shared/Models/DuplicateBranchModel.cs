#nullable disable
namespace Shared.Models;

public class DuplicateBranchModel
{
    public string ProjectName { get; set; }
    public string RepoName { get; set; }
    public string BranchName { get; set; }
    public string CommitName { get; set; }
    public string ComitterEmail { get; set; }
    public string Commiter { get; set; }
}
