namespace Bookmarks.Web.ViewModels.Bookmark
{
    using Infrastructure.Mapping;

    using Data.Models;

    public class TagInputModel : IMapFrom<Tag>
    {
        public string Name { get; set; }
    }
}