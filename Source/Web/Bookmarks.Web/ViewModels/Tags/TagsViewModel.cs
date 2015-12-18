namespace Bookmarks.Web.ViewModels.Tags
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class TagsViewModel: IMapFrom<Tag>
    {
        public string Name { get; set; }

        public int UsedTimes { get; set; }
    }
}