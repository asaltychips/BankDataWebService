using Microsoft.AspNetCore.Mvc;

namespace bankdatawebservice.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Accounts API is working!");
        }
    }
}