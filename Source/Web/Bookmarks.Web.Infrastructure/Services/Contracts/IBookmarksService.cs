namespace Bookmarks.Web.Infrastructure.Services.Contracts
{
    using Base;
    using Data.Models;
    using System.Collections.Generic;

    public interface IBookmarksService : IService
    {
       void Add(string title, string url, string description, string snapshotBase64String, IEnumerable<Tag> tags, Website website, string userId);

    }
}
