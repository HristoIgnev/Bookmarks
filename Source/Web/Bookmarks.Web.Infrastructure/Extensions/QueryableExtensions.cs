namespace Bookmarks.Web.Infrastructure.Extensions
{
    using System;
    using System.Linq;

    using Data.Models;

    public static class QueryableExtensions
    {
        public static IQueryable<Bookmark> ToFilteredBookmarks(this IQueryable<Bookmark> query, BookmarksFilters filters)
        {
            if (!string.IsNullOrWhiteSpace(filters.WebsiteName))
            {
                query = query.Where(b => b.WebSite.Name == filters.WebsiteName);
            }
            if (!string.IsNullOrWhiteSpace(filters.TagName))
            {
                query = query.Where(b => b.Tags.Any(x => x.Name == filters.TagName));
            }
            if (!string.IsNullOrWhiteSpace(filters.Title))
            {
                query = query.Where(b => b.Title.Contains(filters.Title.ToLower()));
            }

            return query;
        }
    }
}
