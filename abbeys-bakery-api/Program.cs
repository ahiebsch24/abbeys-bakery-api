using Microsoft.AspNetCore.Hosting;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.EntityFrameworkCore;
using abbeys_bakery_api.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure Key Vault
var keyVaultName = $"abbeysbakerykv";
string keyVaultUri = $"https://{keyVaultName}.vault.azure.net/";
var Azure_Client_Id = "948ae5e7-97dc-4bc1-a605-277d26d2fd54";
var Azure_Client_Secret = "Dsn8Q~~DW0YIgoijpu_i7OpHuLP8S0lWYvn_Wbye";
var Azure_Tenant_Id = "a9554aee-d042-4b3e-b926-2c75097a1c77";
DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions
{
    // These settings are used to speed up the loading of KeyVault. This way it can ignore unused credential sources.
    ExcludeAzureCliCredential = true,
    ExcludeAzurePowerShellCredential = true,
    ExcludeEnvironmentCredential = true,
    ExcludeInteractiveBrowserCredential = true,
    ExcludeManagedIdentityCredential = !builder.Environment.IsProduction(),
    ExcludeSharedTokenCacheCredential = true,
    ExcludeVisualStudioCodeCredential = !builder.Environment.IsDevelopment(),
    ExcludeVisualStudioCredential = !builder.Environment.IsDevelopment(),
});
builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), defaultAzureCredential);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigins",
    builder =>
    {
        builder.WithOrigins(
                            "http://localhost:4200"
                            )
                            .AllowAnyHeader()
                            .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
});
builder.Services.AddDbContext<AbbeysBakeryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AbbeysBakeryContext")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
var app = builder.Build();

app.UseCors("AllowAngularOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
