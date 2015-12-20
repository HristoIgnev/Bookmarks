namespace Bookmarks.Web.Infrastructure.Services
{

    using System.Linq;

    using Data.Common.Contracts;
    using Data.Models;
    using Contracts;

    public class TagsService : ITagsService
    {
        private IRepository<Bookmark> bookmarks;

        public TagsService( IRepository<Bookmark> bookmarks)
        {
            this.bookmarks = bookmarks;
        }

        public IQueryable<Tag> AllTagsByUser(string userId)
        {
            var allTags= bookmarks.All().Where(b=>b.UserId == userId).SelectMany(x=>x.Tags).Distinct().OrderBy(x => x.Name);
            return allTags;
        }

        public IQueryable<Tag> GetTagByName(string name, string userId)
        {
            return AllTagsByUser(userId).Where(t => t.Name == name);
        }
        
    }
}
