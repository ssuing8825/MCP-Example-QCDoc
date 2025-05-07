﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using QCDocMCPService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

var builder = Host.CreateEmptyApplicationBuilder(settings: null);
builder.Logging.AddConsole(ConsoleLoggerOptions =>
{
    ConsoleLoggerOptions.LogToStandardErrorThreshold = LogLevel.Trace;
    Console.Error.WriteLine("This is safe for logs");
    Console.Out.WriteLine("{\"jsonrpc\": ... }"); // pure JSON only
});

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<QCDocTools>();

builder.Services.AddHttpClient("QCDocAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5125");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddSingleton<QCDocService>();

await builder.Build().RunAsync();
