using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase.Context
{
    public class SystemDbContext : IdentityDbContext<IdentityUser>
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options) { }

    }
}
