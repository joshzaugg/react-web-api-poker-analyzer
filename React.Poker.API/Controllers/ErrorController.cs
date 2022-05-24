using Microsoft.AspNetCore.Mvc;

namespace React.Poker.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {
        [HttpGet, Route("/error")]
        public IActionResult Error() => Problem();
    }
}
