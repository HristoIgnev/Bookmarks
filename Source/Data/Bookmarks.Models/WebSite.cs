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
        public string Url { get; set; }

        public string FaviconBase64String { get; set; }

        //public byte[] FaviconContent { get; set; }

        //public string FaviconType { get; set; }
    }
}
