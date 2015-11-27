namespace Bookmarks.Data.Common.Contracts
{
    using System.Linq;

    using Bookmarks.Data.Common.Models;

    public interface IDeletableEntityRepository<T> : IRepository<T> where T : class, IDeletableEntity
    {
        IQueryable<T> AllWithDeleted();

        void ActualDelete(T entity);
    }
}
