namespace Bookmarks.Web.ViewModels.Bookmarks
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class ThumbnailBookmarkViewModel : IMapFrom<Bookmark>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Url { get; set; }

        public string SnapshotBase64String { get; set; }
        
        public string FaviconBase64String { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Bookmark, ThumbnailBookmarkViewModel>()
                .ForMember(b => b.FaviconBase64String, opt=> opt.MapFrom(b => b.WebSite.FaviconBase64String));
        }
    }
}