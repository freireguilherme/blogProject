using Blog.Web.Data;
using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories;

public class BlogPostRepository : IBlogPostRepository
{
    private readonly BlogDbContext _blogDbContext;

    public BlogPostRepository(BlogDbContext blogDbContext)
    {
        _blogDbContext = blogDbContext;
    }
    public Task<IEnumerable<BlogPost>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BlogPost?> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<BlogPost> AddAsync(BlogPost blogPost)
    {
        await _blogDbContext.AddAsync(blogPost);
        await _blogDbContext.SaveChangesAsync();

        return blogPost;
    }

    public Task<BlogPost?> UpdateAsync(BlogPost blog)
    {
        throw new NotImplementedException();
    }

    public Task<BlogPost?> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}