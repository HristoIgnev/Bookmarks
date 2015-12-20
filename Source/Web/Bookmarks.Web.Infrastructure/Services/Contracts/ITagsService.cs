namespace Bookmarks.Web.Infrastructure.Services.Contracts
{
    using Base;
    using Data.Models;
    using System.Linq;

    public interface ITagsService : IService
    {
        IQueryable<Tag> GetTagByName(string name, string userIds);

        IQueryable<Tag> AllTagsByUser(string userId);
    }
}
