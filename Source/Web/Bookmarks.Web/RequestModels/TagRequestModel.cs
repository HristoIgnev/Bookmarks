namespace Bookmarks.Web.RequestModels
{
    using Infrastructure.Mapping;

    using Data.Models;

    public class TagRequestModel : IMapFrom<Tag>
    {
        public string Name { get; set; }
    }
}