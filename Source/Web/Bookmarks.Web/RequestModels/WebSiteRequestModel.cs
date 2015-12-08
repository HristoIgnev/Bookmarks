namespace Bookmarks.Web.RequestModels
{
    using Bookmarks.Data.Models;
    using Bookmarks.Web.Infrastructure.Mapping;

    public class WebSiteRequestModel : IMapFrom<Website>
    {
        public string Name { get; set; }

        public string FaviconBase64String { get; set; }
    }
}