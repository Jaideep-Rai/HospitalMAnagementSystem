using DTO.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Common.DataContext
{
    // Added by Gautam
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Modules> Modules { get; set; }
        public DbSet<ModuleOperations> ModulesOperations { get; set; }
        public DbSet<RolesPermissionMapper> RolesPermissionMappers { get; set; }
        public DbSet<UserPermissionsMapper> UserPermissionMappers { get; set; }
        public DbSet<ModulePermission> ModulePermissions { get; set; }
        public DbSet<RecentActions> RecentActions { get; set; }
    }
}
