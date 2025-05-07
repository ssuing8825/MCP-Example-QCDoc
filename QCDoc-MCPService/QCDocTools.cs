using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

namespace QCDocMCPService;

[McpServerToolType]
public sealed class QCDocTools
{
    private readonly QCDocService qcdocService;

    public QCDocTools(QCDocService qcdocService)
    {
        this.qcdocService = qcdocService;
    }
    
    [McpServerTool, Description("Get a list of vendors.")]
    public async Task<string> GetVendors()
    {
        var vendors = await qcdocService.GetVendorsAsync();
        return await Task.Run(() => JsonSerializer.Serialize(vendors));
    }

    [McpServerTool, Description("Create a vendor given a vendor object. Returns the VendorId string.")]
    public async Task<string> CreateVendor(
        [Description("The name of the vendor to get details for")] string vendorName,
         [Description("The contact name of the vendor")] string contactName, 
         [Description("Whether or not the vendor is active in the system")]bool isActive)
    {
       var  randomId = new Random().Next(1, 100000);
        var vendor = new Vendor
        {
            Id = randomId, // Assuming Id is a Guid, you can adjust this as needed
            Name = vendorName,
            ContactName = contactName,
            IsActive = isActive,
        };
        
        await qcdocService.CreateVendorAsync(vendor);
        return JsonSerializer.Serialize(randomId.ToString());
    }

    [McpServerTool, Description("Get a single vendor by vendor id.")]
    public async Task<string> GetVendorByVendorId(   [Description("The id or number of the vendor")] int vendorId)
    {
       return  JsonSerializer.Serialize(await qcdocService.GetVendorByIdAsync(vendorId));
    }

    [McpServerTool, Description("Reset Context. To the original state which has 5 vendors")]
    public async Task<string> Reset()
    {
       await qcdocService.ResetAsync();
       return JsonSerializer.Serialize("Vendors reset successfully.");
    }

    [McpServerTool, Description("Echoes the message back to the client.")]
    public static string Echo(string message) => $"Hello from C#: {message}";

    [McpServerTool, Description("Get a list of products.")]
    public async Task<string> GetProducts()
    {
        var products = await qcdocService.GetProductsAsync();
        return await Task.Run(() => JsonSerializer.Serialize(products));
    }

    [McpServerTool, Description("Create a product given a product object. Returns the ProductId string.")]
    public async Task<string> CreateProduct(
        [Description("The name of the product to create")] string productName,
        [Description("The vendor id of the product")] int vendorId,
        [Description("The category of the product")] string category,   
        [Description("The quality status of the product")] string qualityStatus)

    {
        var newProductId = new Random().Next(1, 100000);
        var product = new Product
        {
            Id = newProductId,
            Name = productName,
            VendorId = vendorId,
            Category = category,
            QualityStatus = qualityStatus,
  
        };

        await qcdocService.CreateProductAsync(product);
        return JsonSerializer.Serialize(newProductId.ToString());
    }

    [McpServerTool, Description("Get a single product by product id.")]
    public async Task<string> GetProductById(
        [Description("The id or number of the product")] int productId)
    {
        return JsonSerializer.Serialize(await qcdocService.GetProductByIdAsync(productId));
    }
}