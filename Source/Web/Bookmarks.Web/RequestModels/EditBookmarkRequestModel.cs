namespace Bookmarks.Web.RequestModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Bookmarks.Web.Infrastructure.Mapping;
    using System.Linq;
    using Data.Models;
    using Infrastructure.Extensions;
    public class EditBookmarkRequestModel : IMapFrom<Bookmark>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        
        public List<string> Tags { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Bookmark, EditBookmarkRequestModel>()
                .ForMember(t => t.Tags, opt => opt.MapFrom(b =>  b.Tags.Select(t=>t.Name).ToList()));
        }
    }
}