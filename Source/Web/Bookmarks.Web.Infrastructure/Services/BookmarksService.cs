namespace Bookmarks.Web.Infrastructure.Services
{
    using Data.Common.Contracts;
    using Data.Models;
    using Contracts;
    using System.Linq;
    using System.Collections.Generic;

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
            var existingWebsite = websiteService.GetWebsiteByName(website.Name).FirstOrDefault();
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
                var currentTag = tagService.GetTagByName(tag.Name).FirstOrDefault();
                if (currentTag != null)
                {
                    currentTag.UsedTimes++;
                    bookmark.Tags.Add(currentTag);
                }
                else
                {
                    var tagToAdd = new Tag { Name = tag.Name };
                    tagToAdd.UsedTimes = InfrastructureConstants.TagUsedTimesDefautValue;
                    bookmark.Tags.Add(tagToAdd);
                }
            }

            this.bookmarks.Add(bookmark);
            this.bookmarks.SaveChanges();
        }

        public IQueryable<Bookmark> GetAllByTagName(string tagName)
        {
            var tag = tagService.GetTagByName(tagName).FirstOrDefault();

            if(tag != null)
            {
                return bookmarks.All().Where(b => tag.Name == tagName);
            }

            return null;
        }

        public IQueryable<Bookmark> All()
        {
            return bookmarks.All();
        }
        
        public bool Exist(string url)
        {
            bool checkForUrl = bookmarks.All().Any(b => b.Url == url);

            return checkForUrl;
        }
    }
}
