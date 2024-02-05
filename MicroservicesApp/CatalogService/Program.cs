using CatalogService.Database;
using CatalogService.Services.Interfaces;
using CatalogService.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using CatalogService.Helpers;
using Azure.Identity;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);
// Add keyvault services to the container.
//builder.Services.AddAzureClients(azureClientFactoryBuilder =>
//{
//    var valutUri = new Uri(builder.Configuration["VaultUri"]);
//    azureClientFactoryBuilder.AddSecretClient(valutUri).WithCredential(new DefaultAzureCredential());
//});
//builder.Services.AddSingleton<IKeyVaultService, KeyVaultService>();

//var vault = builder.Services.BuildServiceProvider().GetRequiredService<IKeyVaultService>();
//var connectionString = vault.GetSecret("CatalogDbConnection").Result;

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
////Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(connectionString));

// Add services to the container.
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.PropertyNamingPolicy = null;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICatalogServiceRepository, CatalogServiceRepository>();
builder.Services.AddScoped<IFileHelper, FileHelper>();

var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
