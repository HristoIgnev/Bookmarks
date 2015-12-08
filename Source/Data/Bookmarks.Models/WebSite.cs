namespace Bookmarks.Data.Models
{
    using Common;
    using System.ComponentModel.DataAnnotations;

    public class Website
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(ValidationConstants.MaxWebSiteName)]
        [MinLength(ValidationConstants.MinWebSiteName)]
        public string Name { get; set; }

        public string FaviconBase64String { get; set; }
    }
}
