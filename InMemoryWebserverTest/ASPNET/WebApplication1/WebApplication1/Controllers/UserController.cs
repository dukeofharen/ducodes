using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Business;

namespace WebApplication1.Controllers
{
   [RoutePrefix("users")]
   public class UserController : ApiController
   {
      private readonly IUserManager _userManager;

      public UserController(IUserManager userManager)
      {
         _userManager = userManager;
      }

      [HttpGet]
      [Route("{userId:int}")]
      public HttpResponseMessage GetUser(int userId)
      {
         var user = _userManager.GetUser(userId);
         var response = user == null ?
            Request.CreateResponse(HttpStatusCode.NotFound) :
            Request.CreateResponse(HttpStatusCode.OK, user);
         return response;
      }

      [HttpGet]
      [Route("")]
      public HttpResponseMessage GetUsers()
      {
         var users = _userManager.GetUsers();
         return Request.CreateResponse(HttpStatusCode.OK, users);
      }
   }
}