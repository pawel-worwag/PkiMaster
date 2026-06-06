using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PkiMaster.Infrastructure.Identity;

namespace PkiMaster.Infrastructure.Persistence;

public sealed class PkiMasterIdentityDbContext(DbContextOptions<PkiMasterIdentityDbContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options);
