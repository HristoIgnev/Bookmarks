

namespace Bookmarks.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using System.Web.Mvc;

    public class BaseController : Controller
    {
        public string UserId
        {
            get
            {
                return this.User.Identity.GetUserId();
            }
        }
    }
}