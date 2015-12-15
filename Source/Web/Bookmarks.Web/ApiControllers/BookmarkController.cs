namespace Bookmarks.Web.ApiControllers
{
    using System.Web.Http;
    using System.Web.Http.Cors;
    using Microsoft.AspNet.Identity;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data.Models;
    using Data.Common.Contracts;

    using RequestModels;
    using Infrastructure.Services.Contracts;
    using System.Collections.Generic;

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BookmarkController : ApiController
    {
        private IBookmarksService bookmarkService;

        public BookmarkController(IBookmarksService bookmarkService)
        {
            this.bookmarkService = bookmarkService;
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult CreateBookmark(BookmarkRequestModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return BadRequest("Bookmark not added");
            }

            if (bookmarkService.Exist(model.Url))
            {
                return BadRequest("Bookmark already added");
            }

            var userId = this.User.Identity.GetUserId();
            var tags = Mapper.Map<IEnumerable<TagRequestModel>, IEnumerable<Tag>>(model.Tags);
            var website = Mapper.Map<Website>(model.Website);

            bookmarkService.Add(model.Title, model.Url, model.Description, model.SnapshotBase64String, tags, website, userId);

            return Ok("bookmark added");
        }
    }
}
