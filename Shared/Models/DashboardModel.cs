namespace Shared.Models;

public class DashboardModel
{
    public int BranchCount { get; set; }
    public int ProjectCount { get; set; }
    public int RepositoryCount { get; set; }
    public int StaleBranchesCount { get; set; }
    public int ProjectsWithMultipleBranchesCount { get; set; }
}