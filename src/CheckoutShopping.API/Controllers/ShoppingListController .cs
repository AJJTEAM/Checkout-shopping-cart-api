using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CheckoutShopping.Service.Interfaces;
using CheckoutShopping.Service.Models;
using Microsoft.AspNetCore.Authorization;

namespace CheckoutShopping.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class ShoppingListController : Controller
    {
        private readonly IShoppingListAppService _shoppingListAppService;
        private readonly ILogger<ShoppingListController> _logger;

        public ShoppingListController(IShoppingListAppService shoppingListAppService, ILogger<ShoppingListController> logger)
        {
            _shoppingListAppService = shoppingListAppService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Calling GetAsync from ShoppingListController");
            var response = await _shoppingListAppService.GetAsync();
            if (response == null) return NotFound();
            return Ok(response);
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByProductName(string name)
        {
            _logger.LogInformation("Calling GetByProductNameAsync Drink from ShoppingListController  with name: {0}", name);
            var response = await _shoppingListAppService.GetByProductNameAsync(name);
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ShoppingItem request)
        {
            _logger.LogInformation("Calling AddAsync Drink from ShoppingListController  with request: {@0}", request);
            if (!ModelState.IsValid) return BadRequest();
            await _shoppingListAppService.AddAsync(request);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]ShoppingItem request)
        {
            _logger.LogInformation("Calling UpdateAsync Drink from ShoppingListController  with request: {@0}", request);
            if (!ModelState.IsValid) return BadRequest();
            var response = await _shoppingListAppService.UpdateAsync(request);
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpDelete("{shoppingItemId}")]
        public async Task<IActionResult> Remove(int shoppingItemId)
        {
            _logger.LogInformation("Calling RemoveAsync Drink from ShoppingListController  with shoppingItemId: {0}", shoppingItemId);
            await _shoppingListAppService.RemoveAsync(shoppingItemId);
            return Ok();
        }
    }
}
