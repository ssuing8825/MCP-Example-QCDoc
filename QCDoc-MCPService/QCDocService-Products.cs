using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QCDocMCPService;

public partial class QCDocService
{
    List<Product> productList = new();

    public async Task<List<Product>> GetProductsAsync()
    {
        var response = await httpClient.GetAsync("/api/ProductCatalog");
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();
        productList = JsonSerializer.Deserialize<List<Product>>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new List<Product>();

        foreach (var product in productList)
        {
            Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, VendorId: {product.VendorId}, Category: {product.Category}, QualityStatus: {product.QualityStatus}");
        }
        return productList;
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        var response = await httpClient.GetAsync($"/api/ProductCatalog/{productId}");
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();
        var product = JsonSerializer.Deserialize<Product>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new Product();
        return product;
    }

    public async Task CreateProductAsync(Product product)
    {
        var response = await httpClient.PostAsJsonAsync("/api/ProductCatalog", product);
        response.EnsureSuccessStatusCode();
        Console.WriteLine($"Product created successfully: {product.Name}");
    }
}

