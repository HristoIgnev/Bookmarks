namespace Bookmarks.Web.Infrastructure.Services.Contracts
{
    using System.Linq;
    
    using Base;
    using Data.Models;
    
    public interface IWebsiteService : IService
    {
        int GetIdByName(string name);

        IQueryable<Website> GetWebsiteByName(string name);
    }
}