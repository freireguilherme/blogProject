using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories;

public interface IBlogPostRepository
{
    Task<IEnumerable<BlogPost>> GetAllAsync();
    Task<BlogPost?> GetAsync(Guid id);
    Task<BlogPost> AddAsync(BlogPost blog);
    Task<BlogPost?> UpdateAsync(BlogPost blog);
    Task<BlogPost?> DeleteAsync(Guid id);
}