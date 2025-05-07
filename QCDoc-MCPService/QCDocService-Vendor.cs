using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QCDocMCPService;

public partial class QCDocService
{
    List<Vendor> vendorList = new();
    public async Task<List<Vendor>> GetVendorsAsync()
    {
        var response = await httpClient.GetAsync("/api/VendorManagement");
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();
        vendorList = JsonSerializer.Deserialize<List<Vendor>>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new List<Vendor>();

        foreach (var vendor in vendorList)
        {
            Console.WriteLine($"Id: {vendor.Id}, Name: {vendor.Name}, Contact: {vendor.ContactName}, IsActive: {vendor.IsActive}");
        }
        return vendorList;
    }

    public async Task<Vendor> GetVendorByIdAsync(int vendorId)
    {
        var response = await httpClient.GetAsync($"/api/VendorManagement/{vendorId}");
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();
        var vendor = JsonSerializer.Deserialize<Vendor>(jsonString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new Vendor();
        return vendor;
    }
    public async Task CreateVendorAsync(Vendor vendor)
    {
        var response = await httpClient.PostAsJsonAsync("/api/VendorManagement", vendor);
        response.EnsureSuccessStatusCode();
        Console.WriteLine($"Vendor created successfully: {vendor.Name}");
    }

    public async Task ResetAsync()
    {
        var response = await httpClient.PostAsync("/api/VendorManagement/Reset", null);
        response.EnsureSuccessStatusCode();
        Console.WriteLine("Vendors reset successfully.");
    }

}

