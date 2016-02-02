namespace Bookmarks.Web.Infrastructure.Services
{
    using Data.Common.Contracts;
    using Data.Models;
    using Contracts;
    using System.Linq;
    using System.Collections.Generic;

    using Extensions;
    using System;
    using System.Text;
    using System.Text.RegularExpressions;
    public class BookmarksService : IBookmarksService
    {
        private IDeletableRepository<Bookmark> bookmarks;
        private ITagsService tagService;
        private IWebsiteService websiteService;

        public BookmarksService(IDeletableRepository<Bookmark> bookmarks, ITagsService tagService, IWebsiteService websiteService)
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

            AddTags(bookmark, tags, userId);

            this.bookmarks.Add(bookmark);
            this.bookmarks.SaveChanges();
        }

        public Bookmark Edit(int id, string title, string description, string tags,string userId)
        {
            var bookmark = bookmarks.Include(x => x.WebSite).Where(b => b.UserId == userId && b.Id == id).FirstOrDefault();
            
            bookmark.Title = title;
            bookmark.Description = description;

            bookmark.Tags.Clear();
            if (!string.IsNullOrWhiteSpace(tags))
            {
                var tagsToAdd = tags.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var tag in tagsToAdd)
                {
                    var tagToAdd = new Tag { Name = tag.ToLower() };
                    bookmark.Tags.Add(tagToAdd);
                }
            }
            
            this.bookmarks.Update(bookmark);
            this.bookmarks.SaveChanges();
            
            return bookmark;
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
            return bookmarks.All().Where(b => b.UserId == userId);
        }

        public bool Exist(string url, string userId)
        {
            bool checkForUrl = AllBookmarksByUserId(userId).Any(b => b.Url == url);

            return checkForUrl;
        }

        public IQueryable<Bookmark> GetBookmarkByName(int id, string userId)
        {
            return AllBookmarksByUserId(userId).Where(b => b.Id == id);
        }

        private void AddTags(Bookmark bookmark, IEnumerable<Tag> tags, string userId)
        {
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
        }

        public void Remove(string title, string userId)
        {
            var bookmark = bookmarks.Include(x => x.WebSite).Where(b => b.Title == title && b.UserId == userId).FirstOrDefault();

            bookmark.IsDeleted = true;

            this.bookmarks.Update(bookmark);
            this.bookmarks.SaveChanges();
        }
    }
}
