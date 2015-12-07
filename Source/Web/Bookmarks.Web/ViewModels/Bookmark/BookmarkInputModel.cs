namespace Bookmarks.Web.ViewModels.Bookmark
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Data.Models;
    using Infrastructure.Mapping;

    public class BookmarkInputModel : IMapFrom<Bookmark>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public WebSiteRequestModel Website { get; set; }

        public string Description { get; set; }

        public string SnapshotBase64String { get; set; }

        public IEnumerable<TagInputModel> Tags { get; set; }
    }
}