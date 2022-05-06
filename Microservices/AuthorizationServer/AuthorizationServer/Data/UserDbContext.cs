using AuthorizationServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationServer.Data;

public class UserDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
    }
}