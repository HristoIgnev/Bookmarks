namespace Bookmarks.Web.ViewModels.Tags
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class TagsViewModel: IMapFrom<Tag>, IHaveCustomMappings
    {
        public string Name { get; set; }
        
        public int UsedTimes { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {

            configuration.CreateMap<Tag, TagsViewModel>()
                .ForMember(t=> t.UsedTimes, opt=> opt.MapFrom(t =>t.Bookmarks.Count));
        }
    }
}