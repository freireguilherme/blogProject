using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Controllers
{   
    [Authorize(Roles = "Admin")]
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
    
    [HttpPost]
    public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
    {
        //map view model back to domain model
        var blogPostDomainModel = new BlogPost
        {
            Id = editBlogPostRequest.Id,
            Heading = editBlogPostRequest.Heading,
            PageTitle = editBlogPostRequest.PageTitle,
            Content = editBlogPostRequest.Content,
            ShortDescription = editBlogPostRequest.ShortDescription,
            FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
            UrlHandle = editBlogPostRequest.UrlHandle,
            PublishedDate = editBlogPostRequest.PublishedDate,
            Author = editBlogPostRequest.Author,
            Visible = editBlogPostRequest.Visible,

        };
        //map tags into domain model
        var selectTags = new List<Tag>();
        foreach (var selectedTag in editBlogPostRequest.SelectedTags)
        {
            if (Guid.TryParse(selectedTag, out var tag))
            {
                var foundTag = await _tagRepository.GetAsync(tag);

                if (foundTag != null)
                {
                    selectTags.Add(foundTag);
                }
            }
        }

        blogPostDomainModel.Tags = selectTags;
        
        //Submit information to repository to update
        var updatedBlog = await _blogPostRepository.UpdateAsync(blogPostDomainModel);

        if (updatedBlog != null)
        {
            //Show success notification
            return RedirectToAction("Edit");
        }
        
        //Show an error notification
        return RedirectToAction("Edit");
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
    {
        //talk to repository to delete this blog and tags
        var deletedBlogPost = await _blogPostRepository.DeleteAsync(editBlogPostRequest.Id);

        if (deletedBlogPost != null)
        {
            //Show success notification
            return RedirectToAction("List");
        }
        // Show error notification
        return RedirectToAction("Edit", new { id = editBlogPostRequest.Id });
        
    }
}    
}

