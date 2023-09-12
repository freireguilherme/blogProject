using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BlogDbContext _blogDbContext;

        public AdminTagsController(BlogDbContext blogDbContext)
        {
            this._blogDbContext = blogDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult SubmitTag(AddTagRequest addTagRequest)
        {
            // mapping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName

            };

            _blogDbContext.Tags.Add(tag);
            _blogDbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            //use Bdcontext to read the tags
            var tags = _blogDbContext.Tags.ToList();
            
            return View(tags);
        }
    }
}
