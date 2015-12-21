namespace Bookmarks.Web.ViewModels.Bookmarks
{
    using Data.Models;
    using System.Collections.Generic;
    using Web.Infrastructure.Mapping;

    public class BookmarkDetailViewModel : IMapFrom<Bookmark>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public Website WebSite { get; set; }

        public string Description { get; set; }

        public string SnapshotBase64String { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }

}