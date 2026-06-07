using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace PkiMaster.Backend.Components.Shared;

public partial class UserDisplayComponent(AuthenticationStateProvider authenticationStateProvider) : ComponentBase
{
    private string? userDisplayName;
     
    protected override async Task OnInitializedAsync()
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        userDisplayName = user.Identity?.Name;
    }
}