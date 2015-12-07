namespace Bookmarks.Web.ApiControllers
{
    using System.Web.Http;

    public class ExtensionController : ApiController
    {

        [HttpGet]
        public object GetCurrentUser()
        {
            var currentUser = RequestContext.Principal.Identity;
            if (!currentUser.IsAuthenticated)
            {
                return new
                {
                    Logged = false,
                    Name = ""
                };
            }
            
            return new {
                Logged = true, 
                Name = currentUser.Name
            };
        }

    }
}
