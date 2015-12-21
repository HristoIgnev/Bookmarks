namespace Bookmarks.Web.Controllers
{
    using System.Web.Mvc;

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if(this.UserId != null)
            {
                return RedirectToAction("Index", "Bookmarks");
            }

            return View();
        }                
    }
}