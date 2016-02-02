namespace Bookmarks.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.Services.Contracts;
    using AutoMapper.QueryableExtensions;
    using ViewModels.Bookmarks;

    using RequestModels;

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
        [Route("Bookmarks/ByTag/{*name}")]
        public ActionResult ByTag(string name)
        {          
            var result = bookmarks.GetBookmarksByTagName(name, this.UserId).ProjectTo<ThumbnailBookmarkViewModel>().ToList();
            return View("Index", result);
        }

        [Authorize]
        [Route("Bookmarks/ByWebsite/{name}/")]
        public ActionResult ByWebsite(string name)
        {
            var result = bookmarks.GetBookmarksByWebsiteName(name, this.UserId).ProjectTo<ThumbnailBookmarkViewModel>().ToList();

            return View("Index", result);
        }

        public ActionResult Details(int id, string title)
        {
            var result = bookmarks.GetBookmarkByName(id, this.UserId).ProjectTo<BookmarkDetailViewModel>().FirstOrDefault();
            return View(result);
        }

        [HttpPost]
        public ActionResult Remove(string title)
        {
            bookmarks.Remove(title, this.UserId);
            return Content("k");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id )
        {
            var bookmark = bookmarks.GetBookmarkByName(id, this.UserId).ProjectTo<EditBookmarkRequestModel>().FirstOrDefault();
            return View(bookmark);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditBookmarkRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            var bookmark = bookmarks.Edit(model.Id, model.Title, model.Description, model.Tags[0], this.UserId);

            return RedirectToAction("Details", new {id = bookmark.Id, title = bookmark.Title });
        }
    }
}