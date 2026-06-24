using PkiMaster.Application.Common.Messaging;
using PkiMaster.Application.Identity.Abstractions;
using PkiMaster.Dto.Identity.GetAllUsers;

namespace PkiMaster.Application.Identity;

public record GetAllUsersRequest : IRequest<ICollection<Dto.Identity.GetAllUsers.User>>
{
    
}

internal class GetAllUsersHandler (IIdentityUserRepository repo)
    : IHandler<GetAllUsersRequest, ICollection<Dto.Identity.GetAllUsers.User>>
{
    public async Task<ICollection<User>> HandleAsync(GetAllUsersRequest query, CancellationToken cancellationToken = default)
    {
        return (await repo.ListAsync(cancellationToken)).Select(user => new Dto.Identity.GetAllUsers.User()
        {
            Id =  user.Id,
            UserName = user.UserName,
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
            AccessFailedCount = user.AccessFailedCount,
            LockoutEnabled = user.LockoutEnabled,
            LockoutEnd = user.LockoutEnd,
            Roles = user.Roles
        }).ToList();
    }
}