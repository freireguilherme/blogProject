using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories;

public class BlogPostRepository : IBlogPostRepository
{
    private readonly BlogDbContext _blogDbContext;

    public BlogPostRepository(BlogDbContext blogDbContext)
    {
        _blogDbContext = blogDbContext;
    }
    public async Task<IEnumerable<BlogPost>> GetAllAsync()
    {
        return await _blogDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
    }

    public async Task<BlogPost?> GetAsync(Guid id)
    {
        return await _blogDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<BlogPost> AddAsync(BlogPost blogPost)
    {
        await _blogDbContext.AddAsync(blogPost);
        await _blogDbContext.SaveChangesAsync();

        return blogPost;
    }

    public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
    {
        var existingBlog = await _blogDbContext.BlogPosts
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

        if (existingBlog != null)
        {
            existingBlog.Id = blogPost.Id;
            existingBlog.Heading = blogPost.Heading;
            existingBlog.PageTitle = blogPost.PageTitle;
            existingBlog.Content = blogPost.Content;
            existingBlog.ShortDescription = blogPost.ShortDescription;
            existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
            existingBlog.UrlHandle = blogPost.UrlHandle;
            existingBlog.PublishedDate = blogPost.PublishedDate;
            existingBlog.Author = blogPost.Author;
            existingBlog.Visible = blogPost.Visible;
            existingBlog.Tags = blogPost.Tags;

            await _blogDbContext.SaveChangesAsync();
            return existingBlog;
        }

        return null;
    }

    public Task<BlogPost?> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}