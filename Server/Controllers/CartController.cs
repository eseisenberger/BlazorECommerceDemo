using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorECommerceDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("products")]
        public async Task<ActionResult<ServiceResponse<List<CartProductDTO>>>> GetCartProducts(List<CartItem> cartItems)
        {
            var results = await _cartService.GetCartProducts(cartItems);
            return Ok(results);
        }
        
        [HttpPost("add")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddToCart(CartItem cartItem)
        {
            var results = await _cartService.AddToCart(cartItem);
            return Ok(results);
        }
        
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<CartProductDTO>>>> StoreCardItems(List<CartItem> cartItems)
        {
            var results = await _cartService.StoreCartItems(cartItems);
            return Ok(results);
        }
        [HttpPut("update-quantity")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateQuantity(CartItem cartItem)
        {
            var results = await _cartService.UpdateQuantity(cartItem);
            return Ok(results);
        }
        [HttpDelete("{productId}/{productTypeId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> RemoveItemFromCart(int productId, int productTypeId)
        {
            var results = await _cartService.RemoveItemFromCart(productId, productTypeId);
            return Ok(results);
        }
        [HttpGet("count")]
        public async Task<ActionResult<ServiceResponse<int>>> GetCartItemsCount()
        {
            var result = await _cartService.GetCartItemsCount();
            return result;
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<CartProductDTO>>>> GetDbCartProducts()
        {
            var result = await _cartService.GetDbCartProducts();
            return Ok(result);
        }
    }
}
