﻿namespace Bookmarks.Web.Infrastructure.Services
{

    using System.Linq;

    using Data.Common.Contracts;
    using Data.Models;
    using Contracts;

    public class TagsService : ITagsService
    {
        private IRepository<Tag> tags;
        private IRepository<Bookmark> bookmarks;

        public TagsService(IRepository<Tag> tags, IRepository<Bookmark> bookmarks)
        {
            this.tags = tags;
            this.bookmarks = bookmarks;
        }

        public IQueryable<Tag> AllTagsByUser(string userId)
        {
            var allTags = bookmarks.All().Where(b=>b.UserId == userId).SelectMany(x=>x.Tags);
            return allTags;
        }

        public int GetIdByName(string name)
        {
            return this.tags.All().Where(t => t.Name == name).FirstOrDefault().Id;
        }

        public IQueryable<Tag> GetTagByName(string name, string userId)
        {
            return AllTagsByUser(userId).Where(t => t.Name == name);
        }
    }
}
