namespace Bookmarks.Data.Common.Contracts
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }
    }
}
