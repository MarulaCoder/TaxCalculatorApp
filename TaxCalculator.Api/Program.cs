using System.Text.Json.Serialization;
using TaxCalculator.Application;
using TaxCalculator.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    // ignore omitted parameters on models to enable optional params (e.g. User update)
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

using (var context = new AppDbContextContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<AppDbContext>>()))
{
    // Look for any movies.
    if (!context.TaxRates.Any())
    {
        var taxRates = TaxSeedData.GetTaxRates();
        context.TaxRates.AddRange(taxRates);
    }

    if (!context.TaxTypes.Any())
    { 
        var taxTypes = TaxSeedData.GetTaxTypes();
        context.TaxTypes.AddRange(taxTypes);
    }

    if (context.FlatValueTax == null)
    {
        var flatValueTax = TaxSeedData.GetFlatValueTax();
        context.FlatValueTax.Add(flatValueTax);
    }

    if (context.FlatRateTax == null)
    {
        var flatValueTax = TaxSeedData.GetFlatRateTax();
        context.FlatRateTax.Add(flatRateTax);
    }

    context.SaveChanges();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(); 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// app.Run("http://localhost:4000");
app.Run();
