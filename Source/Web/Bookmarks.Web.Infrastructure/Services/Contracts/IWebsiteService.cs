namespace Bookmarks.Web.Infrastructure.Services.Contracts
{
    using System.Linq;
    
    using Base;
    using Data.Models;
    
    public interface IWebsiteService : IService
    {
        IQueryable<Website> GetWebsiteByName(string name, string userId);

        IQueryable<Website> AllWebsitesByUser(string userId);
    }
}