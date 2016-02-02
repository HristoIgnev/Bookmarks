namespace Bookmarks.Data.Common.Contracts
{
    using System.Linq;

    public interface IDeletableRepository<T> : IRepository<T> where T : class, IDeletableEntity
    {
        IQueryable<T> AllWithDeleted();

        IQueryable<T> AllDeleted();

        void ActualDelete(T entity);
    }
}
