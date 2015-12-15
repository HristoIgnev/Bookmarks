namespace Bookmarks.Web.Controllers
{
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var userId = this.User.Identity.GetUserId();

            if(userId != null)
            {
                return RedirectToAction("Index", "Bookmarks");
            }

            return View();
        }                
    }
}