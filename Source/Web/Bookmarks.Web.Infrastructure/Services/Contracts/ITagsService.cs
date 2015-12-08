namespace Bookmarks.Web.Infrastructure.Services.Contracts
{
    using Base;
    using Data.Models;
    using System.Linq;

    public interface ITagsService : IService
    {
        int GetIdByName(string name);

        IQueryable<Tag> GetTagByName(string name);
    }
}
