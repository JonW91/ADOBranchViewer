using Dapper;
using Model.Context;
using Model.Contracts;
using Shared.DTO.Projects;
using Shared.Models;

namespace Model.Repository;

public class ProjectRepository : IProjectRepository
{
    private readonly IDapperContext _context;

    public ProjectRepository(IDapperContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectModel>> GetProjects()
    {
        const string query =
            "SELECT [Id], [Name], [Url], [LastUpdateTime], [Description] FROM [AdoBranchViewer].[dbo].[Projects]";
        using var connection = _context.CreateConnection();
        var projects = await connection.QueryAsync<ProjectModel>(query);
        return projects.ToList();
    }

    public async Task<List<Guid>> GetProjectIds()
    {
        const string query =
            "SELECT [Id] FROM [AdoBranchViewer].[dbo].[Projects]";
        using var connection = _context.CreateConnection();
        var projects = await connection.QueryAsync<Guid>(query);
        return projects.ToList();
    }

    public async Task InsertProjectData(AdoProjectsHolder? adoProjectsHolder)
    {
        var projects = await GetProjectIds();

        // only insert projects that don't previously exist
        foreach (var project in adoProjectsHolder?.Value?.Where(o => !projects.Contains(Guid.Parse(o.Id))))
        {
            project.LastUpdateTime = DateTime.Now; // Some reason they're coming back as datetime.min (to fix)
            const string query = "INSERT INTO [AdoBranchViewer].[dbo].[Projects]" +
                                 "([Id],[Name],[Description]," +
                                 "[Url],[State], [Revision], [Visibility], [LastUpdateTime])" +
                                 " Values " +
                                 "(@Id, @Name, @Description, " +
                                 "@Url,@State, @Revision, @Visibility, @LastUpdateTime)";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, project);
        }
    }
}