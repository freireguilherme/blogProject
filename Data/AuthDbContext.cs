using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Data;

public class AuthDbContext : IdentityDbContext
{
    public  AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        //seed roles (User, Admin, SuperAdmin)
        const string adminRoleId = "97447a0e-33a4-4f69-87b8-5985770f31ff";
        const string superAdminRoleId = "7c0cc981-f196-445a-9f22-5a32633c612b";
        const string userRoleId = "9238d3d3-2d5f-4aeb-a68a-a1311e5e4cb9";
        
        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "Name",
                Id = adminRoleId,
                ConcurrencyStamp = adminRoleId
            },
            
            new IdentityRole
            {
                Name = "SuperAdmin",
                NormalizedName = "SuperName",
                Id = superAdminRoleId,
                ConcurrencyStamp = superAdminRoleId
            },
            
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "user",
                Id = userRoleId,
                ConcurrencyStamp = userRoleId
            }
        };
        
        //Entity FrameworkCore will insert these roles inside the database
        builder.Entity<IdentityRole>().HasData(roles);

        //Seed SuperAdminUser
        const string superAdminId = "d786bb42-c3f3-4718-a874-0ebce583dea9";
        var superAdminUser = new IdentityUser
        {
            UserName = "superadmin@blog.com",
            Email = "superadmin@blog.com",
            NormalizedUserName = "superadmin@blog.com".ToUpper(),
            NormalizedEmail = "superadmin@blog.com".ToUpper(),
            Id = superAdminId
        };

        superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
            .HashPassword(superAdminUser, "Superadmin@123");

        builder.Entity<IdentityUser>().HasData(superAdminUser);

        //Add all roles to SuperAdminUser
        var superAdminRoles = new List<IdentityUserRole<string>>
        {
            new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = superAdminId,
            },
            new IdentityUserRole<string>
            {
                RoleId = superAdminRoleId,
                UserId = superAdminId,
            },
            new IdentityUserRole<string>
            {
                RoleId = userRoleId,
                UserId = superAdminId,
            },
        };

        builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
    }
}