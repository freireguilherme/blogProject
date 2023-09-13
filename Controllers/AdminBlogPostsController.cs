using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Controllers;

public class AdminBlogPostsController : Controller
{
    private readonly ITagRepository _tagRepository;

    public AdminBlogPostsController(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
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
        
        return RedirectToAction("Add");
    }
}