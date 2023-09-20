using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories;

public interface IBlogPostLikeRepository
{
    Task<int> GetTotalLikes(Guid blogPostId);
    Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);
}