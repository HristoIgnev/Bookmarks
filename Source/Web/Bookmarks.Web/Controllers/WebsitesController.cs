namespace Bookmarks.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Bookmarks.Web.ViewModels.Websites;
    using Bookmarks.Web.Infrastructure.Services.Contracts;

    public class WebsitesController : BaseController
    {
        private IWebsiteService websites;
        public WebsitesController(IWebsiteService websites)
        {
            this.websites = websites;
        }

        [HttpGet]
        [Authorize]
        public ActionResult All()
        {
            var allWebsites = websites.AllWebsitesByUser(this.UserId).ProjectTo<WebsiteViewModel>().ToList();
            return View(allWebsites);
        }
    }
}