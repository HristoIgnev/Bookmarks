namespace Bookmarks.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.Services.Contracts;
    using AutoMapper.QueryableExtensions;
    using ViewModels.Bookmarks;
    using Microsoft.AspNet.Identity;
    using System.Web.Http;

    using Infrastructure;
    public class BookmarksController : Controller
    {
        private IBookmarksService bookmarks;
        public BookmarksController(IBookmarksService bookmarks)
        {
            this.bookmarks = bookmarks;
        }
        public ActionResult Index()
        {
            var allBookmarks = bookmarks.AllBookmarksByUserId(this.User.Identity.GetUserId()).ProjectTo<ThumbnailBookmarkViewModel>().ToList();
            return View(allBookmarks);
        }

        public ActionResult All()
        {
            return View();
        }
        
        public ActionResult ByTag(string name)
        {
            var userId = this.User.Identity.GetUserId();
            var result = bookmarks.GetBookmarksByTagName(name, userId).ProjectTo<ThumbnailBookmarkViewModel>().ToList();

            TempData["TagName"] = name;

            return View("Index", result);
        }
        public ActionResult ByWebsite(string name)
        {
            var userId = this.User.Identity.GetUserId();
            var result = bookmarks.GetBookmarksByWebsiteName(name, userId).ProjectTo<ThumbnailBookmarkViewModel>().ToList();

            TempData["WebsiteName"] = name;

            return View("Index", result);
        }
        public ActionResult Search([FromUri]BookmarksFilters query)
        {
            var userId = this.User.Identity.GetUserId();
            var result = bookmarks.Search(query, userId).ProjectTo<ThumbnailBookmarkViewModel>().ToList();
               
            return this.PartialView("_BookmarkResult", result);
        }
    }
}