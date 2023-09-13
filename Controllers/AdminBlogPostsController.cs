using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers;

public class AdminBlogPostsController : Controller
{
    public  IActionResult Add()
    {
        return View();
    }
}