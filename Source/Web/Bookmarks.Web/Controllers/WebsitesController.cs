using Bookmarks.Web.Infrastructure.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Bookmarks.Web.ViewModels.Websites;
using AutoMapper.QueryableExtensions;

namespace Bookmarks.Web.Controllers
{
    public class WebsitesController : Controller
    {
        private IWebsiteService websites;
        public WebsitesController(IWebsiteService websites)
        {
            this.websites = websites;
        }

        public ActionResult All()
        {
            var allWebsites = websites.AllWebsitesByUser(User.Identity.GetUserId()).ProjectTo<WebsiteViewModel>().ToList();
            return View(allWebsites);
        }
    }
}