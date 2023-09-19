using Blog.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories;

public class BlogPostLikeRepository : IBlogPostLikeRepository
{
    private readonly BlogDbContext _blogDbContext;

    public BlogPostLikeRepository(BlogDbContext blogDbContext)
    {
        _blogDbContext = blogDbContext;
    }
    public async Task<int> GetTotalLikes(Guid blogPostId)
    {
        return await _blogDbContext.BlogPostLike
            .CountAsync(x => x.BlogPostId == blogPostId);
    }
}