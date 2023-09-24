using Blog.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuthDbContext _authDbContext;

    public UserRepository(AuthDbContext authDbContext)
    {
        _authDbContext = authDbContext;
    }
    public async Task<IEnumerable<IdentityUser>> GetAll()
    {
        var users = await _authDbContext.Users.ToListAsync();
        var superAdminUser = await _authDbContext.Users
            .FirstOrDefaultAsync(x => x.Email == "superadmin@blog.com");

        if (superAdminUser is not null)
        {
            users.Remove(superAdminUser);
        }

        return users;
    }
}