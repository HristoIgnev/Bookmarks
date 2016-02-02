namespace Bookmarks.Web.Infrastructure.Services.Contracts
{
    using Base;
    using Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public interface IBookmarksService : IService
    {
        void Add(string title, string url, string description, string snapshotBase64String, IEnumerable<Tag> tags, Website website, string userId);

        Bookmark Edit(int id, string title, string description, string tags, string userId);

        IQueryable<Bookmark> AllBookmarksByUserId(string userId);

        bool Exist(string url, string userId);

        void Remove(string title,string userId);
        
        IQueryable<Bookmark> GetBookmarkByName(int id, string userId);

        IQueryable<Bookmark> GetBookmarksByTagName(string tagName, string userId);

        IQueryable<Bookmark> GetBookmarksByWebsiteName(string websiteName, string userId);
                
    }
}
