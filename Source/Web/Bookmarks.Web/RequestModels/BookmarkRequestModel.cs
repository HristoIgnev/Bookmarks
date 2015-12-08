namespace Bookmarks.Web.RequestModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Data.Models;
    using Infrastructure.Mapping;

    public class BookmarkRequestModel : IMapFrom<Bookmark>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public WebSiteRequestModel Website { get; set; }

        public string Description { get; set; }

        public string SnapshotBase64String { get; set; }

        public IEnumerable<TagRequestModel> Tags { get; set; }
    }
}