using Microsoft.AspNetCore.Mvc;
using ProductInventory.Api.ViewModels;

namespace ProductInventory.Api.Controllers
{
    [Route("user")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(UserResponse), 200)]
        public IActionResult FindUser()
        {
            return Ok(UserResponse.CreateUser());
        }
    }
}