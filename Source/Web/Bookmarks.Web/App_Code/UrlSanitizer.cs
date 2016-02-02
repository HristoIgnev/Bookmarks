namespace Bookmarks.Web.App_Code
{
    using System.Web.Mvc;
    using System.Text.RegularExpressions;
    using System.Text;
    public static class UrlSanitizer
    {
        public static string Sanitize(this HtmlHelper htmlHelper, string title)
        {
            string cleanTitle = title.ToLower().Trim().Replace(" ", "-");
            //Removes invalid characters
            cleanTitle = Regex.Replace(Regex.Replace(cleanTitle, @"[^\w\s\][-]", ""), @"[-]{2,}", "-");
            return cleanTitle;
        }
       
    }
}