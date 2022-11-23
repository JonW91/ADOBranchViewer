namespace Shared.DTO.Projects;

public class AdoProjectsHolder
{
    public int Count { get; set; }
    public List<AdoProjects>? Value { get; set; }
}

// Records guaruntee immutability. This data must remain unchanged so the app can present factual information.