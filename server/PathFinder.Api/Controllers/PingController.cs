using Microsoft.AspNetCore.Mvc;

namespace PathFinder.Api.Controllers
{
    [ApiController]
    [Route("ping")]
    public class PingController : Controller
    {
        [HttpGet]
        public ActionResult<string> Ping()
        {
            return Ok("pong");
        }
    }
}