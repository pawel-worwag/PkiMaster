using Microsoft.AspNetCore.Components;
using PkiMaster.Application.Common.Messaging;
using PkiMaster.Application.Identity;

namespace PkiMaster.Backend.Components.Pages.Identity;

public partial class Users(IHandler<GetAllUsersRequest, ICollection<Dto.Identity.GetAllUsers.User>> handler) : ComponentBase
{
    private IEnumerable<Dto.Identity.GetAllUsers.User> _users = new List<Dto.Identity.GetAllUsers.User>();
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _users = await handler.HandleAsync(new GetAllUsersRequest());
    }
}