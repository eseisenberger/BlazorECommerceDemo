namespace BlazorECommerceDemo.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action ProductsChanged;
        List<Product> Products { get; set; }
        public string Message { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        string LastSearchText { get; set; }
        Task GetProductsAsync(string? categoryUrl = null);
        Task<ServiceResponse<Product>> GetProductAsync(int productId);
        Task SearchProducts(string searchText, int page);
        Task<List<string>> GetProductSearchSuggestions(string searchText);
    }
}
