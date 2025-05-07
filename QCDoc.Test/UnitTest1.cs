using QCDocMCPService;

namespace QCDoc.Test;

public class UnitTest1
{
    string BaseAddress = "http://localhost:5125"; // Replace with the actual service URL
    public UnitTest1()
    {
        // Initialization logic for tests can be added here if needed.
    }
    
    [Fact]
    public async Task GetVendorsAsync_ReturnsVendorList()
    {
        // Arrange
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(BaseAddress) // Replace with the actual service URL
        };

        var service = new QCDocService(new HttpClientFactoryStub(httpClient));

        // Act
        var result = await service.GetVendorsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.All(result, vendor => Assert.NotNull(vendor.Name));
    }

    [Fact]
    public async Task CreateVendor()
    {
        // Arrange
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseAddress) 
            };
               var service = new QCDocService(new HttpClientFactoryStub(httpClient));
           
           var randomVendorId = new Random().Next(1, 100000);
            var vendor = new Vendor
            {
                Id = randomVendorId, // Assuming Id is a Guid, you can adjust this as needed
                Name = "Test Vendor",
                ContactName = "Test Contact",
                IsActive = true,
            };

            Console.WriteLine($"Random Vendor ID: {randomVendorId}");

         await service.CreateVendorAsync(vendor);

             // Act
        var result = await service.GetVendorByIdAsync(randomVendorId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(randomVendorId, result.Id);
    }

    [Fact]
    public async Task GetVendorByIdAsync_ReturnsVendor()
    {
        // Arrange
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(BaseAddress) // Replace with the actual service URL
        };

        var service = new QCDocService(new HttpClientFactoryStub(httpClient));
        int vendorId = 1; // Replace with a valid vendor ID for testing

        // Act
        var result = await service.GetVendorByIdAsync(vendorId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(vendorId, result.Id);
    }


    private class HttpClientFactoryStub : IHttpClientFactory
    {
        private readonly HttpClient _httpClient;

        public HttpClientFactoryStub(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpClient CreateClient(string name) => _httpClient;
    }
}
