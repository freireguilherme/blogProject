using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers;

public class BlogsController : Controller
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IBlogPostLikeRepository _blogPostLikeRepository;

    public BlogsController(IBlogPostRepository blogPostRepository,
        IBlogPostLikeRepository blogPostLikeRepository)
    {
        _blogPostRepository = blogPostRepository;
        _blogPostLikeRepository = blogPostLikeRepository;
    }
    [HttpGet]
    public async Task<IActionResult> Index(string urlHandle)
    {
        var blogPost = await _blogPostRepository.GetByUrlHandleAsync(urlHandle);
        var blogPostLikeViewModel = new BlogDetailsViewModel();
       

        if (blogPost != null)
        {
            var totalLikes = await _blogPostLikeRepository.GetTotalLikes(blogPost.Id);
             blogPostLikeViewModel = new BlogDetailsViewModel
            {
                Id = blogPost.Id,
                Heading = blogPost.Heading,
                PageTitle = blogPost.PageTitle,
                Content = blogPost.Content,
                ShortDescription = blogPost.ShortDescription,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                Visible = blogPost.Visible,
                Tags = blogPost.Tags,
                TotalLikes = totalLikes
            };
        }
        return View(blogPostLikeViewModel);
    }
}