namespace Bookmarks.Data.Common.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Bookmarks.Data.Common.Contracts;

    public class DeletableEntityRepository<T> : EfGenericRepository<T>, IDeletableRepository<T>
        where T : class, IDeletableEntity
    {
        public DeletableEntityRepository(DbContext context)
            : base(context)
        {
        }

        public override IQueryable<T> All()
        {
            return base.All().Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return base.All();
        }

        public override void Delete(T entity)
        {
            entity.IsDeleted = true;

            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void ActualDelete(T entity)
        {
            base.Delete(entity);
        }

        public IQueryable<T> AllDeleted()
        {
            return base.All().Where(x => x.IsDeleted);
        }
    }
}