using FrankIS.MyReporter.Management.Contracts;
using FrankIS.MyReporter.Management.Models;
using Microsoft.AspNetCore.Components;

namespace FrankIS.MyReporter.Webclient.Pages;

public partial class OpenProjectLinks
{
    [Inject] private IOpenProjectManager? OpenProjectManager { get; set; }

    private bool _loading;
    private OpenProjectResponse? _response;

    protected override async Task OnParametersSetAsync()
    {
        _loading = true;

        var openProjectResponse = await OpenProjectManager!.GetWorkspaceAsync(8295);

        _loading = false;
    }
}
