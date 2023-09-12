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
        public IActionResult Add(AddTagRequest addTagRequest)
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

        [HttpGet]
        [ActionName("Edit")]
        public IActionResult Edit(Guid id)
        {
            //var tag = _blogDbContext.Tags.Find(id);
            var tag = _blogDbContext.Tags.FirstOrDefault(x => x.Id == id);
            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
            return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var existingTag = _blogDbContext.Tags.Find(tag.Id);
            
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                
                //saving changes
                _blogDbContext.SaveChanges();
                
                //Show success notification
                return RedirectToAction("List");
            }
            //Show error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
        [HttpPost]
        public IActionResult Delete(EditTagRequest editTagRequest)
        {
            var tag = _blogDbContext.Tags.Find(editTagRequest.Id);
            if (tag != null)
            {
                _blogDbContext.Tags.Remove(tag);
                _blogDbContext.SaveChanges();

                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
    }
}
