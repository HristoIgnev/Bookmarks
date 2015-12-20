namespace Bookmarks.Web.Infrastructure.Services
{
    using Data.Common.Contracts;
    using Data.Models;
    using Contracts;
    using System.Linq;
    using System.Collections.Generic;

    using Extensions;
    using System;

    public class BookmarksService : IBookmarksService
    {
        private IRepository<Bookmark> bookmarks;
        private ITagsService tagService;
        private IWebsiteService websiteService;

        public BookmarksService(IRepository<Bookmark> bookmarks, ITagsService tagService, IWebsiteService websiteService)
        {
            this.bookmarks = bookmarks;
            this.tagService = tagService;
            this.websiteService = websiteService;
        }

        public void Add(string title, string url, string description, string snapshotBase64String, IEnumerable<Tag> tags, Website website, string userId)
        {     
            var bookmark = new Bookmark
            {
                Description = description,
                UserId = userId,
                Title = title,
                Url = url,
                SnapshotBase64String = snapshotBase64String

            };            

            //check for existing website 
            var existingWebsite = websiteService.GetWebsiteByName(website.Name, userId).FirstOrDefault();
            if (existingWebsite != null)
            {
                bookmark.WebSite = existingWebsite;
            }
            else
            {
                var websiteToAdd = new Website
                {
                    Name = website.Name,
                    FaviconBase64String = website.FaviconBase64String
                };

                bookmark.WebSite = website;
            }
            
            foreach (var tag in tags)
            {
                var currentTag = tagService.GetTagByName(tag.Name, userId).FirstOrDefault();
                if (currentTag != null)
                {
                    bookmark.Tags.Add(currentTag);
                }
                else
                {
                    var tagToAdd = new Tag { Name = tag.Name.ToLower() };
                    bookmark.Tags.Add(tagToAdd);
                }
            }

            this.bookmarks.Add(bookmark);
            this.bookmarks.SaveChanges();
        }

        public IQueryable<Bookmark> GetBookmarksByTagName(string tagName, string userId)
        {
            var bookmarksByTagName = AllBookmarksByUserId(userId).Where(b => b.Tags.Any(x => x.Name == tagName));
            return bookmarksByTagName;
        }
        public IQueryable<Bookmark> GetBookmarksByWebsiteName(string websiteName, string userId)
        {
            var bookmarksByWebsiteName = AllBookmarksByUserId(userId).Where(b => b.WebSite.Name == websiteName);
            return bookmarksByWebsiteName;
        }

        public IQueryable<Bookmark> AllBookmarksByUserId(string userId)
        {
            return bookmarks.All().Where(b=>b.UserId == userId);
        }

        public bool Exist(string url, string userId)
        {
            bool checkForUrl = AllBookmarksByUserId(userId).Any(b => b.Url == url);

            return checkForUrl;
        }

        public IQueryable<Bookmark> Search(BookmarksFilters filters, string userId)
        {
            if (filters == null)
            {
                return AllBookmarksByUserId(userId);
            }
            return AllBookmarksByUserId(userId).ToFilteredBookmarks(filters);
        }
    }
}
