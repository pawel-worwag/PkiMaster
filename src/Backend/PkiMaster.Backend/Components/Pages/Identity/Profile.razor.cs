using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace PkiMaster.Backend.Components.Pages.Identity;

public partial class Profile(AuthenticationStateProvider authenticationStateProvider) : ComponentBase
{
    private string? userDisplayName;
    private IEnumerable<Claim> _claims = new List<Claim>();
    
    protected override async Task OnInitializedAsync()
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        userDisplayName = user.Identity?.Name;
        _claims = user.Claims;
    }
}