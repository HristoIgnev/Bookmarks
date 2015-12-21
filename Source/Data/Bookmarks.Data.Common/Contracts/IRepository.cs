namespace Bookmarks.Data.Common.Contracts
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> All();

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(object id);

        T Attach(T entity);

        void Detach(T entity);

        int SaveChanges();

        IQueryable<T> Include<TProparty>(Expression<Func<T, TProparty>> conditions);

        Task<int> SaveChangesAsync();
    }
}
