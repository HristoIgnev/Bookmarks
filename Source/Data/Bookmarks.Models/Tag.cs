
namespace Bookmarks.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tag
    {
        private ICollection<Bookmark> bookmarks;

        public Tag()
        {
            this.bookmarks = new HashSet<Bookmark>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int UsedTimes { get; set; }

        public virtual ICollection<Bookmark> Bookmarks
        {
            get
            {
                return this.bookmarks;
            }
            set
            {
                this.bookmarks = value;
            }
        }

    }
}