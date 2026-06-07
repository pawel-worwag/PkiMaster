using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PkiMaster.Infrastructure.Identity;

namespace PkiMaster.Backend.Components.Pages.Identity;

public partial class Users(UserManager<ApplicationUser> userManager) : ComponentBase
{
    private IEnumerable<ApplicationUser> _users = new List<ApplicationUser>();
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _users = await userManager.Users.OrderBy(p=>p.NormalizedUserName).ToListAsync();
    }
}