namespace Shared.Models;

public class ProjectModel
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
    public DateTime? LastUpdateTime { get; set; }
    public string? Description { get; set; }
}