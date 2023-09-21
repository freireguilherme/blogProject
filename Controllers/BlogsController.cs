using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers;

public class BlogsController : Controller
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IBlogPostLikeRepository _blogPostLikeRepository;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public BlogsController(IBlogPostRepository blogPostRepository,
        IBlogPostLikeRepository blogPostLikeRepository,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager)
    {
        _blogPostRepository = blogPostRepository;
        _blogPostLikeRepository = blogPostLikeRepository;
        _signInManager = signInManager;
        _userManager = userManager;
    }
    [HttpGet]
    public async Task<IActionResult> Index(string urlHandle)
    {
        var liked = false;
        var blogPost = await _blogPostRepository.GetByUrlHandleAsync(urlHandle);
        var blogPostLikeViewModel = new BlogDetailsViewModel();
       

        if (blogPost != null)
        {
            var totalLikes = await _blogPostLikeRepository.GetTotalLikes(blogPost.Id);

            if (_signInManager.IsSignedIn(User))
            {
                //get like for this blog for this user
                var likesForBlog = await _blogPostLikeRepository.GetLikesForBlog(blogPost.Id);
                var userId = _userManager.GetUserId(User);

                if (userId != null)
                {
                    var likeFromUser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                    liked = likeFromUser != null;
                }
            }
            
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
                TotalLikes = totalLikes,
                Liked = liked
            };
        }
        return View(blogPostLikeViewModel);
    }
}