namespace Bookmarks.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.Services.Contracts;
    using AutoMapper.QueryableExtensions;
    using ViewModels.Bookmarks;
    using Data.Models;
    using System.Collections.Generic;

    public class BookmarksController : Controller
    {
        private IBookmarksService bookmarks;
        public BookmarksController(IBookmarksService bookmarks)
        {
            this.bookmarks = bookmarks;
        }
        public ActionResult Index()
        {
            var allBookmarks = bookmarks.All().ProjectTo<ThumbnailBookmarkViewModel>().ToList();
            return View(allBookmarks);
        }

        public ActionResult All()
        {
            return View();
        }
    }
}