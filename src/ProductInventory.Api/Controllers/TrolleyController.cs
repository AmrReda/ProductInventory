using Microsoft.AspNetCore.Mvc;
using ProductInventory.Domain.Queries.CalculateTrolley;

namespace ProductInventory.Api.Controllers
{
    [Route("trolleyTotal")]
    public class TrolleyController : ControllerBase
    {
        private readonly CalculateTrolleyQueryHandler _calculateTrolleyQueryHandler;

        public TrolleyController(CalculateTrolleyQueryHandler calculateTrolleyQueryHandler)
        {
            _calculateTrolleyQueryHandler = calculateTrolleyQueryHandler;
        }

        [HttpPost]
        public ActionResult<double> TrolleyTotal(
            [FromBody] CalculateTrolleyQuery query
        )
        {
            return Ok(_calculateTrolleyQueryHandler.Handle(query));
        }
    }
}