using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QCDocMCPService;

public partial class QCDocService
{
    private readonly HttpClient httpClient;
    public QCDocService(IHttpClientFactory httpClientFactory)
    {
        httpClient = httpClientFactory.CreateClient("QCDocAPI");
    }

}

