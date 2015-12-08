namespace Bookmarks.Web.Infrastructure.Services
{

    using System.Linq;

    using Data.Common.Contracts;
    using Data.Models;
    using Contracts;

    public class TagsService : ITagsService
    {
        private IRepository<Tag> tags;

        public TagsService(IRepository<Tag> tags)
        {
            this.tags = tags;
        }

        public IQueryable<Tag> GetTagByName(string name)
        {
            return this.tags.All().Where(tg => tg.Name == name);
        }

        public int GetIdByName(string name)
        {
            return this.tags.All().Where(t => t.Name == name).FirstOrDefault().Id;
        }
    }
}
