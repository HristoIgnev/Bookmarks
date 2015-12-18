namespace Bookmarks.Web.Infrastructure.Services.Contracts
{
    using Base;
    using Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public interface IBookmarksService : IService
    {
        void Add(string title, string url, string description, string snapshotBase64String, IEnumerable<Tag> tags, Website website, string userId);

        IQueryable<Bookmark> AllBookmarksByUserId(string userId);

        bool Exist(string url, string userId);

        IQueryable<Bookmark> GetBookmarksByTagName(string tagName, string userId);

        IQueryable<Bookmark> Search(string query, string userId);
        
    }
}
