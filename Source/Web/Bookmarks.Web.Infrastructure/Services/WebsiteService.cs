namespace Bookmarks.Web.Infrastructure.Services
{
    using Data.Common.Contracts;
    using Data.Models;

    using Contracts;
    using System.Linq;

    public class WebsiteService : IWebsiteService
    {
        private IRepository<Website> websites;

        public WebsiteService(IRepository<Website> websites)
        {
            this.websites = websites;
        }

        public int GetIdByName(string name)
        {
            return this.websites.All().Where(w => w.Name == name).FirstOrDefault().Id;
        }

        public IQueryable<Website> GetWebsiteByName(string name)
        {
            return this.websites.All().Where(w => w.Name == name);
        }
    }
}
