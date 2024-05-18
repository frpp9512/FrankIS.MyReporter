using FrankIS.MyReporter.Management.Models;

namespace FrankIS.MyReporter.Management.Contracts;
public interface IOpenProjectManager
{
    Task<OpenProjectResponse?> GetWorkspaceAsync(int workspaceId, CancellationToken cancellationToken = default);
}
