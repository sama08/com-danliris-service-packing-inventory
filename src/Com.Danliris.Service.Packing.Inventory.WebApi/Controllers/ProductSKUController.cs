using System;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.ProductSKU;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Packing.Inventory.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("v1/product-skus")]
    [Authorize]
    public class ProductSKUController : Controller
    {
        private readonly IProductSKUService _service;
        private readonly IIdentityProvider _identityProvider;

        public ProductSKUController(IProductSKUService service, IIdentityProvider identityProvider)
        {
            _service = service;
            _identityProvider = identityProvider;
        }

        protected void VerifyUser()
        {
            _identityProvider.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            _identityProvider.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
            _identityProvider.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.ReadById(id);
            return Ok(new
            {
                data = result,

            });
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string keyword, [FromQuery] int page = 1, [FromQuery] int size = 25)
        {
            var result = await _service.ReadByQuery(page, size, keyword);
            return Ok(new
            {
                data = result.Data,
                info = new
                {
                    total = result.TotalRow,
                    page = page,
                    size = size
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductSKUViewModel viewModel)
        {
            VerifyUser();
            if (!ModelState.IsValid)
            {
                var result = new
                {
                    error = ResultFormatter.FormatErrorMessage(ModelState)
                };
                return new BadRequestObjectResult(result);
            }

            await _service.Create(viewModel);

            return Created("/", new { });
        }

        [HttpPut("{id}")]
        public IActionResult Put()
        {
            VerifyUser();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            VerifyUser();
            await _service.Delete(id);
            return NoContent();
        }
    }


}