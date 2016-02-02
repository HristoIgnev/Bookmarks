namespace Bookmarks.Web.ViewModels.Bookmarks
{
    using System.Collections.Generic;

    using Data.Models;
    using Web.Infrastructure.Mapping;
    using Tags;
    using Websites;
    public class BookmarkDetailViewModel : IMapFrom<Bookmark>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public WebsiteViewModel WebSite { get; set; }

        public string Description { get; set; }

        public string SnapshotBase64String { get; set; }

        public virtual ICollection<TagsViewModel> Tags { get; set; }
    }
}