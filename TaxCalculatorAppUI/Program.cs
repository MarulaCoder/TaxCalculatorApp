using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaxCalculatorAppUI;
using TaxCalculatorAppUI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//var baseAddress = builder.HostEnvironment.BaseAddress;
//https://localhost:7206/swagger/index.html
//"applicationUrl": "https://localhost:7206;http://localhost:5203",

//var baseAddress = @"http://localhost:5203/";
var baseAddress = @"https://localhost:7206/";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });

builder.Services.AddHttpClient("TaxCalculatorApi", client =>
    client.BaseAddress = new Uri(baseAddress));

builder.Services.AddScoped<ITaxService, TaxService>();

await builder.Build().RunAsync();
