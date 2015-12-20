namespace Bookmarks.Web.Infrastructure.Services
{
    using Data.Common.Contracts;
    using Data.Models;

    using Contracts;
    using System.Linq;

    public class WebsiteService : IWebsiteService
    {
        private IRepository<Bookmark> bookmarks;

        public WebsiteService( IRepository<Bookmark> bookmarks)
        {
            this.bookmarks = bookmarks;
        }

        public IQueryable<Website> AllWebsitesByUser(string userId)
        {
            var allWebsites = bookmarks.All().Where(b => b.UserId == userId).Select(x => x.WebSite).Distinct();
            return allWebsites;
        }

        public IQueryable<Website> GetWebsiteByName(string name, string userId)
        {
            return this.AllWebsitesByUser(userId).Where(w => w.Name == name);
        }
    }
}
