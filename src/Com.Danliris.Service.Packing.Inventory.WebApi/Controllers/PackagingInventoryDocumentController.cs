using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.PackagingInventoryDocument;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/packaging-inventory-documents")]
    public class PackagingInventoryDocumentController : Controller
    {
        public PackagingInventoryDocumentController(IPackagingInventoryDocumentService service)
        {

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(new { });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(new { });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePackagingInventoryDocumentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var result = new 
                {
                    error = ResultFormatter.FormatErrorMessage(ModelState)
                };
                return new BadRequestObjectResult(result);
            }


            
            return Created("/", new { });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put()
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete()
        {
            return NoContent();
        }
    }

    
}