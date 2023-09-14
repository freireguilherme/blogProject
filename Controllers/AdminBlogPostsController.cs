using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Controllers;

public class AdminBlogPostsController : Controller
{
    private readonly ITagRepository _tagRepository;
    private readonly IBlogPostRepository _blogPostRepository;

    public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
    {
        _tagRepository = tagRepository;
        _blogPostRepository = blogPostRepository;
    }
    [HttpGet]
    public async  Task<IActionResult> Add()
    {
        //get tags from repository
        var tags = await _tagRepository.GetAllAsync();
        var model = new AddBlogPostRequest
        {
            Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
        };
        
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
    {
        //map view model to domain model
        var blogPost = new BlogPost()
        {
            Heading = addBlogPostRequest.Heading,
            PageTitle = addBlogPostRequest.PageTitle,
            Content = addBlogPostRequest.Content,
            ShortDescription = addBlogPostRequest.ShortDescription,
            FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
            UrlHandle = addBlogPostRequest.UrlHandle,
            PublishedDate = addBlogPostRequest.PublishedDate,
            Author = addBlogPostRequest.Author,
            Visible = addBlogPostRequest.Visible,
        };

        var selectedTags = new List<Tag>();
        //map tags from selected tags
        foreach (var selectedTadId in addBlogPostRequest.SelectedTags)
        {
            var selectedTagIdAsGuid = Guid.Parse(selectedTadId);
            var existingTag = await _tagRepository.GetAsync(selectedTagIdAsGuid);

            if (existingTag != null)
            {
                selectedTags.Add(existingTag);
            }
        }
        //mapping tags back to domain model
        blogPost.Tags = selectedTags;
        
        await _blogPostRepository.AddAsync(blogPost);
        return RedirectToAction("Add");
    }
    
    [HttpGet]
    public async Task<IActionResult> List()
    {
        //call repository
        var blogPosts = await _blogPostRepository.GetAllAsync();
        return View(blogPosts);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var blogPost = await _blogPostRepository.GetAsync(id);
        var tagsDomainModel = await _tagRepository.GetAllAsync();
        if (blogPost != null)
        {
            //map the domain model into the view model
            var model = new EditBlogPostRequest
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
                Tags = tagsDomainModel.Select(x => new SelectListItem
                {
                    //<select> is receiving two values, Text and Value to display onto view
                    Text = x.Name, Value = x.Id.ToString()
                }),
                SelectedTags = blogPost.Tags.Select( x=> x.Id.ToString()).ToArray()
                
            };
                
            return View(model);
        }

        return View(null);
    }
    
}