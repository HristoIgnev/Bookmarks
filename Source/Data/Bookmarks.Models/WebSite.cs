namespace Bookmarks.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class Website
    {
        private ICollection<Bookmark> bookmarks;

        public Website()
        {
            this.bookmarks = new HashSet<Bookmark>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(ValidationConstants.MaxWebSiteName)]
        [MinLength(ValidationConstants.MinWebSiteName)]
        public string Name { get; set; }

        public string FaviconBase64String { get; set; }

        public ICollection<Bookmark> Bookmarks
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
