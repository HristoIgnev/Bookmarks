namespace Bookmarks.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Migrations;
    using Models;

    public class BookmarksDbContext : IdentityDbContext<User>
    {
        public BookmarksDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BookmarksDbContext, Configuration>());
        }

        public virtual IDbSet<Bookmark> Bookmarks { get; set; }

        public static BookmarksDbContext Create()
        {
            return new BookmarksDbContext();
        }
    }
}
