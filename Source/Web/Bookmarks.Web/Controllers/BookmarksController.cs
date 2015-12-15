namespace Bookmarks.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    public class BookmarksController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult All()
        {
            return View();
        }
    }
}