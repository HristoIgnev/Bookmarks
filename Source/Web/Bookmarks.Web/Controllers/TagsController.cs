namespace Bookmarks.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Bookmarks.Web.Infrastructure.Services.Contracts;
    using ViewModels.Tags;

    public class TagsController : BaseController
    {
        private ITagsService tags;
        public TagsController(ITagsService tags)
        {
            this.tags = tags;
        }

        [HttpGet]
        [Authorize]
        public ActionResult All()
        {
            var allTags = tags.AllTagsByUser(this.UserId).ProjectTo<TagsViewModel>().ToList();
            return View(allTags);
        }
    }
}