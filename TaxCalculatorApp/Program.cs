using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TaxCalculatorApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<ITaxCalculatorData, TaxCalculatorData>();

//builder.Services.AddScoped<HttpClient>(s =>
//{
//    return new HttpClient { BaseAddress = new Uri(@"http://localhost:4000/") };
//});

//builder.Services.AddHttpClient();

builder.Services.AddHttpClient("TaxCalculatorApi", c =>
{
    c.BaseAddress = new Uri("http://localhost:4000/");
    c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
