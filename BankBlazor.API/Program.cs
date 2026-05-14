using BankBlazor.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder (args);

// Add services to the container.
builder.Services.AddDbContext<BankBlazor.API.Data.BankBlazorContext> (options =>
    options.UseSqlServer (builder.Configuration.GetConnectionString ("DefaultConnection") 
    ?? throw new InvalidOperationException ("Connection string 'DefaultConnection' not found.")));

builder.Services.AddControllers ();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddSwaggerGen ();


//Metod för att iggnorera cirkulär referens i Json-serialisering
//TAS BORT SENARE NÄR DTO ÄR SKAPADE!!!!
builder.Services.AddControllers ().AddJsonOptions (options =>
{
    options.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddScoped<TransactionValidationService> ();

var app = builder.Build ();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment ())
{
    app.UseSwagger ();
    app.UseSwaggerUI ();
}

app.UseHttpsRedirection ();

app.UseAuthorization ();

app.MapControllers ();

app.Run ();
