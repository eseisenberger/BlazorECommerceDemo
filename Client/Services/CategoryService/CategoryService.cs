namespace BlazorECommerceDemo.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private HttpClient _http;

        public CategoryService(HttpClient http)
        {
            _http = http;
        }
        public List<Category> Categories { get; set; } = new();

        public async Task GetCategories()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/category");
            if(result != null && result.Data != null)
                Categories = result.Data;
        }
    }
}
