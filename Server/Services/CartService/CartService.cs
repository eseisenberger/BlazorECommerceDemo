using BlazorECommerceDemo.Shared;
using System.Security.Claims;

namespace BlazorECommerceDemo.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public CartService(DataContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }
        public async Task<ServiceResponse<List<CartProductDTO>>> GetCartProducts(List<CartItem> cartItems)
        {
            var result = new ServiceResponse<List<CartProductDTO>>()
            {
                Data = new List<CartProductDTO>()
            };

            foreach(var item in cartItems)
            {
                var cartProduct = await GetDtoFromDbAsync(item);
                if(cartProduct != null)
                    result.Data.Add(cartProduct);
            }

            return result;
        }

        public async Task<ServiceResponse<List<CartProductDTO>>> StoreCartItems(List<CartItem> cartItems)
        {
            var userId = _authService.GetUserId();
            cartItems.ForEach(item => item.UserId = userId);
            _context.CartItems.AddRange(cartItems);
            await _context.SaveChangesAsync();

            return await GetDbCartProducts();
        }

        private async Task<CartProductDTO?> GetDtoFromDbAsync(CartItem item)
        {
            var product = await _context.Products
                    .Where(p => p.Id == item.ProductId)
                    .FirstOrDefaultAsync();

            if (product == null)
                return null;

            var productVariant = await _context.ProductVariants
                    .Where(v => v.ProductId == item.ProductId && v.ProductTypeId == item.ProductTypeId)
                    .Include(v => v.ProductType)
                    .FirstOrDefaultAsync();

            if (productVariant == null)
                return null;

            var cartProduct = new CartProductDTO()
            {
                ProductId = product.Id,
                Title = product.Title,
                ImageUrl = product.ImageUrl,
                Price = productVariant.Price,
                ProductType = productVariant.ProductType.Name,
                ProductTypeId = productVariant.ProductTypeId,
                Quantity = item.Quantity
            };

            return cartProduct;
        }

        public async Task<ServiceResponse<int>> GetCartItemsCount()
        {
            var count = (await _context.CartItems.Where(item => item.UserId == _authService.GetUserId()).ToListAsync()).Sum(x => x.Quantity);
            return new ServiceResponse<int> { Data = count };
        }

        public async Task<ServiceResponse<List<CartProductDTO>>> GetDbCartProducts()
        {
            return await GetCartProducts(await _context.CartItems.Where(item => item.UserId == _authService.GetUserId()).ToListAsync());
        }

        //CRUD
        public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
        {
            cartItem.UserId = _authService.GetUserId();
            var sameItem = await GetCartItem(cartItem.ProductId, cartItem.ProductTypeId);
            if (sameItem == null)
                _context.CartItems.Add(cartItem);
            else
                sameItem.Quantity += cartItem.Quantity;

            await _context.SaveChangesAsync();

            return new() { Data = true };
        }

        public async Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem)
        {
            cartItem.UserId = _authService.GetUserId();

            var dbCartItem = await GetCartItem(cartItem.ProductId, cartItem.ProductTypeId);
            if (dbCartItem == null)
                return new()
                {
                    Data = false,
                    Success = false,
                    Message = "Cart item does not exist." 
                };

            dbCartItem.Quantity = cartItem.Quantity;
            await _context.SaveChangesAsync();

            return new() { Data = true };
        }

        public async Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int productTypeId)
        {
            var dbCartItem = await GetCartItem(productId, productTypeId);

            if (dbCartItem == null)
                return new()
                {
                    Data = false,
                    Success = false,
                    Message = "Cart item does not exist."
                };

            _context.CartItems.Remove(dbCartItem);
            await _context.SaveChangesAsync();

            return new() { Data = true };
        }

        private async Task<CartItem?> GetCartItem(int productId, int productTypeId, int? userId = null)
        {
            if(userId == null)
                userId = _authService.GetUserId();

            var cartItem = await _context.CartItems.FirstOrDefaultAsync(item => item.ProductId == productId && item.ProductTypeId == productTypeId && item.UserId == userId);
            return cartItem;
        }
    }
}
