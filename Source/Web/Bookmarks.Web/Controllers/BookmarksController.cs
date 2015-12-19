namespace Bookmarks.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.Services.Contracts;
    using AutoMapper.QueryableExtensions;
    using ViewModels.Bookmarks;
    using Microsoft.AspNet.Identity;
    using System.Web.Routing;
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
        
        public ActionResult AllWithTag(string tagName)
        {
            var userId = this.User.Identity.GetUserId();
            var result = bookmarks.GetBookmarksByTagName(tagName, userId).ProjectTo<ThumbnailBookmarkViewModel>().ToList();

            return View("Index", result);
        }

        public ActionResult Search(string query)
        {
            var userId = this.User.Identity.GetUserId();
            var result = bookmarks.Search(query, userId).ProjectTo<ThumbnailBookmarkViewModel>().ToList();
               
            return this.PartialView("_BookmarkResult", result);
        }
    }
}