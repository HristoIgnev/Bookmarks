namespace Bookmarks.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Bookmarks.Web.Infrastructure.Services.Contracts;
    using ViewModels.Tags;
    using Microsoft.AspNet.Identity;

    public class TagsController : Controller
    {
        private ITagsService tags;
        public TagsController(ITagsService tags)
        {
            this.tags = tags;
        }

        public ActionResult All()
        {
            var allTags = tags.AllTagsByUser(User.Identity.GetUserId()).ProjectTo<TagsViewModel>().ToList();
            return View(allTags);
        }
    }
}