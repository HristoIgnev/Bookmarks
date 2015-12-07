namespace Bookmarks.Web.ViewModels.Bookmark
{
    using Bookmarks.Data.Models;
    using Bookmarks.Web.Infrastructure.Mapping;

    public class WebSiteRequestModel : IMapFrom<Website>
    {
        public string Url { get; set; }

        public string FaviconBase64String { get; set; }
    }
}