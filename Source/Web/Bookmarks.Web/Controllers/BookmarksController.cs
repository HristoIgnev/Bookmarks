namespace Bookmarks.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.Services.Contracts;
    using AutoMapper.QueryableExtensions;
    using ViewModels.Bookmarks;

    using Infrastructure;
    public class BookmarksController : BaseController
    {
        private IBookmarksService bookmarks;
        public BookmarksController(IBookmarksService bookmarks)
        {
            this.bookmarks = bookmarks;
        }

        public ActionResult Index()
        {
            if (this.UserId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var allBookmarks = bookmarks.AllBookmarksByUserId(this.UserId).ProjectTo<ThumbnailBookmarkViewModel>().ToList();
            return View(allBookmarks);
        }

        [Authorize]
        public ActionResult All()
        {
            return View();
        }

        [Authorize]
        public ActionResult ByTag(string name)
        {          
            var result = bookmarks.GetBookmarksByTagName(name, this.UserId).ProjectTo<ThumbnailBookmarkViewModel>().ToList();

            TempData["TagName"] = name;

            return View("Index", result);
        }

        [Authorize]
        public ActionResult ByWebsite(string name)
        {
            var result = bookmarks.GetBookmarksByWebsiteName(name, this.UserId).ProjectTo<ThumbnailBookmarkViewModel>().ToList();

            TempData["WebsiteName"] = name;

            return View("Index", result);
        }

        public ActionResult Details(string title)
        {
            var result = bookmarks.GetBookmarksByName(title, this.UserId).ProjectTo<BookmarkDetailViewModel>().FirstOrDefault();
            return View(result);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {

            return View();
        }


        [Authorize]
        public ActionResult Search([System.Web.Http.FromUri]BookmarksFilters query)
        {
            var result = bookmarks.Search(query, this.UserId).ProjectTo<ThumbnailBookmarkViewModel>().ToList();

            return this.PartialView("_BookmarkResult", result);
        }
    }
}