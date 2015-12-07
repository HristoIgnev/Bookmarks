namespace Bookmarks.Web.ApiControllers
{
    using Data.Models;
    using Data.Common.Contracts;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using ViewModels.Bookmark;
    using Microsoft.AspNet.Identity;
    using System.Linq;

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BookmarkController : ApiController
    {
        private IRepository<Bookmark> bookmarks;
        private IRepository<Website> websites;
        private IRepository<Tag> tags;


        public BookmarkController(IRepository<Bookmark> bookmarks, IRepository<Website> websites, IRepository<Tag> tags)
        {
            this.bookmarks = bookmarks;
            this.websites = websites;
            this.tags = tags;
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult CreateBookmark(BookmarkInputModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                var userId = this.User.Identity.GetUserId();

                var bookmark = new Bookmark
                {
                    Description = model.Description,
                    UserId = userId,
                    Title = model.Title,
                    Url = model.Url,
                    SnapshotBase64String = model.SnapshotBase64String

                };

                var existingWebsite = websites.All().FirstOrDefault(x => x.Url == model.Website.Url);
                if (existingWebsite != null)
                {
                    bookmark.WebSite = existingWebsite;
                }
                else
                {
                    var website = new Website
                    {
                        Url = model.Website.Url,
                        FaviconBase64String = model.Website.FaviconBase64String
                    };
                    bookmark.WebSite = website;
                }

                var allTags = tags.All().ToList();

                foreach (var tag in model.Tags)
                {
                    var currentTag = allTags.FirstOrDefault(x => x.Name == tag.Name);
                    if (currentTag != null)
                    {
                        bookmark.Tags.Add(currentTag);
                    }
                    else
                    {
                        var tagy = new Tag { Name = tag.Name };

                        bookmark.Tags.Add(tagy);
                    }
                }

                this.bookmarks.Add(bookmark);
                this.bookmarks.SaveChanges();

                return Ok("bookmark added");
            }
            return BadRequest("bookmark not added");
        }
    }
}
