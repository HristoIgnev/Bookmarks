namespace Bookmarks.Web.ViewModels.Websites
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    public class WebsiteViewModel : IMapFrom<Website>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public int UsedTimes { get; set; }

        public string FaviconBase64String { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Website, WebsiteViewModel>()
                .ForMember(w => w.UsedTimes, opt => opt.MapFrom(w => w.Bookmarks.Count));
        }
    }
}