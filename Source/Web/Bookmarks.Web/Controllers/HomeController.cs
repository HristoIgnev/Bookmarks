namespace Bookmarks.Web.Controllers
{
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Data.Models;
    using Data.Common.Contracts;
    using System.Linq;

    public class HomeController : Controller
    {
        private IRepository<Bookmark> bookmarks;

        public HomeController(IRepository<Bookmark> bookmarks)
        {
            this.bookmarks = bookmarks;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Route("About")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Route("Contact")]
        [Authorize]
        public ActionResult Contact()
        {
            var userId = this.User.Identity.GetUserId();

            var allbookmarks = bookmarks.All().Where(u => u.UserId == userId).ToList();

            return View(allbookmarks);
        }
    }
}