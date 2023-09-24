using Microsoft.AspNetCore.Identity;

namespace Blog.Web.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<IdentityUser>> GetAll();
}