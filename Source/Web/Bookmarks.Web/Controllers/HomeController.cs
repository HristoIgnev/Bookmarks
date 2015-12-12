namespace Bookmarks.Web.Controllers
{
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Data.Models;
    using Data.Common.Contracts;
    using System.Linq;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var userId = this.User.Identity.GetUserId();

            return View();
        }                
    }
}